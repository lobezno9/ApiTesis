using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Business.Interfaces;
using Data.Context;
using DataAccess.Interfaces;
using DataAccess.Repository;
using Entities.BE;
using MethodParameters.MP;
using MethodParameters.VM;
using Utilities.Cryptography;
using Microsoft.Extensions.Configuration;

namespace Business.BO
{
    public class UserBO : IUserBO

    {
        IUserRepository _userRepository;
        private readonly IMapper _mapper;
        ProjectContext _context;
        IConfiguration _iConfiguration;
        public UserBO(IMapper mapper, ProjectContext context, IConfiguration Configuration)
        {
            _userRepository = new UserRepository(context);
            _mapper = mapper;
            _context = context;
            _iConfiguration = Configuration;
        }

        public LoginOut Login(LoginIn loginIn)
        {
            loginIn.Password = Cryptography.Encrypt(loginIn.Password);
            UserBE userResultBE = _userRepository.GetAll(_mapper.Map<UserBE>(loginIn)).FirstOrDefault();
            LoginOut result = new LoginOut();
            if (userResultBE != null)
            {
                result = _mapper.Map<LoginOut>(userResultBE);
            }

            if (userResultBE != null)
            {
                IProfileBO profileBO = new ProfileBO(_mapper, _context);
                ProfileVM profileResult = profileBO.GetAll(new GetAllProfileIn()
                {
                    Profile = new ProfileVM() { Id = userResultBE.ProfileId }
                }).ListProfile.FirstOrDefault();
                result.IsSuperAdmin = profileResult.IsSuperAdmin;
                result.IsLoginOk = userResultBE != null;
                result.IsActive = userResultBE.IsActive != false;
            }
            result.Result = MethodParameters.General.Result.Success;
            return result;
        }

        public GetAllUserOut GetAll(GetAllUserIn getAllUserIn)
        {
            GetAllUserOut getAllUserOut = new GetAllUserOut();
            getAllUserIn = getAllUserIn ?? new GetAllUserIn();
            List<UserBE> listUserBE = _userRepository.GetAll(_mapper.Map<UserBE>(getAllUserIn.User ?? new UserVM()));
            List<UserVM> listUserVM = _mapper.Map<List<UserVM>>(listUserBE);

            getAllUserOut.Result = MethodParameters.General.Result.Success;
            getAllUserOut.ListUser = listUserVM.OrderByDescending(x => x.Id).ToList();

            return getAllUserOut;
        }

        public AddUserOut Add(AddUserIn addUserIn)
        {
            AddUserOut addUserOut = new AddUserOut();
            addUserIn.User.FirstName = addUserIn.User.FirstName.ToUpper();
            addUserIn.User.LastName = addUserIn.User.LastName.ToUpper();
            addUserIn.User.ProfileId = addUserIn.User.Profile.Id;
            addUserIn.User.Password = Cryptography.Encrypt(addUserIn.User.Password);
            addUserIn.User.ModificationDate = DateTime.Now;
            int newId = _userRepository.Add(_mapper.Map<UserBE>(addUserIn.User));
            addUserOut.Result = newId > 0 ? MethodParameters.General.Result.Success : MethodParameters.General.Result.Error;
            addUserOut.Id = newId;

            return addUserOut;
        }

        public UpdateUserOut Update(UpdateUserIn updateUserIn)
        {
            UpdateUserOut updateUserOut = new UpdateUserOut();
            updateUserIn.User.FirstName = updateUserIn.User.FirstName.ToUpper();
            updateUserIn.User.LastName = updateUserIn.User.LastName.ToUpper();
            updateUserIn.User.ProfileId = updateUserIn.User.Profile.Id;
            if (updateUserIn.User.Password != null)
            {
                updateUserIn.User.Password = Cryptography.Encrypt(updateUserIn.User.Password);
            }
            updateUserIn.User.ModificationDate = DateTime.Now;
            bool result = _userRepository.Update(_mapper.Map<UserBE>(updateUserIn.User));
            updateUserOut.Result = result ? MethodParameters.General.Result.Success : MethodParameters.General.Result.Error;

            return updateUserOut;
        }

        /// <summary>
        /// Update the password of a client by username encrypted
        /// </summary>
        /// <param name="updateUserIn">Entity with the password and username</param>
        /// <returns>Confirmation of the process</returns>
        public UpdateUserOut UpdatePassword(UpdateUserIn updateUserIn)
        {
            UpdateUserOut updateUserOut = new UpdateUserOut();
            updateUserIn.User.Password = Cryptography.Encrypt(updateUserIn.User.Password);
            updateUserIn.User.Username = Cryptography.Decrypt(updateUserIn.User.Username);
            updateUserIn.User.ModificationDate = DateTime.Now;
            UserVM user = new UserVM()
            {
                Username = updateUserIn.User.Username
            };
            user = _mapper.Map<UserVM>(_userRepository.GetAll(_mapper.Map<UserBE>(user ?? new UserVM())).FirstOrDefault());
            updateUserIn.User.Id = user.Id;
            bool result = _userRepository.Update(_mapper.Map<UserBE>(updateUserIn.User));
            updateUserOut.Result = result ? MethodParameters.General.Result.Success : MethodParameters.General.Result.Error;

            return updateUserOut;
        }

        /// <summary>
        /// Validates if the user name and the email match
        /// </summary>
        /// <param name="userBE">Get the username and email for execute the spr_Users_ValidateRecoverPasswordCommand</param>
        /// <returns>boolean value with true if the username and email match</returns>
        public ValidateRecoverPasswordOut ValidateRecoverPassword(ValidateRecoverPasswordIn validateRecoverPasswordIn)
        {

            ValidateRecoverPasswordOut validateRecoverPasswordOut = new ValidateRecoverPasswordOut();
            bool result = _userRepository.ValidateRecoverPassword(_mapper.Map<UserBE>(validateRecoverPasswordIn.ValidateRecoverPassword));
            validateRecoverPasswordOut.Result = result ? MethodParameters.General.Result.Success : MethodParameters.General.Result.Error;
            validateRecoverPasswordOut.IsConfirmed = result;

            if (result)
            {
                EmailConfigVM emailConfigVM = new EmailConfigVM
                {
                    Host = _iConfiguration["EmailConfiguration:Host"],
                    User = _iConfiguration["EmailConfiguration:User"],
                    Password = _iConfiguration["EmailConfiguration:Password"],
                    Port = Convert.ToInt32(_iConfiguration["EmailConfiguration:Port"]),
                    IsEnabledSsl = Convert.ToBoolean(_iConfiguration["EmailConfiguration:IsEnabledSsl"])
                };

                Utilities.Email.Email email = new Utilities.Email.Email(emailConfigVM);
                string usernameEncrypted = Cryptography.Encrypt(validateRecoverPasswordIn.ValidateRecoverPassword.Username);
                string Subject = "Recuperación de Contraseña";
                string body = " Siga al siguiente link para cambiar su contraseña. <br> http://localhost:4200/UpdatePassword?Username=" + usernameEncrypted + "";
                //string body = " Siga al siguiente link para cambiar su contraseña. <br> http://192.168.2.32/WebCNueva/UpdatePassword?Username=" + usernameEncrypted + "";
                //email.SendEmail(emailConfigVM.User, validateRecoverPasswordIn.ValidateRecoverPassword.Email, _iConfiguration["UpdatePasswordEmail:Subject"], body, true);
                email.SendEmail(emailConfigVM.User, validateRecoverPasswordIn.ValidateRecoverPassword.Email, Subject, body, true);
            }

            return validateRecoverPasswordOut;
        }

        public GetAllUserOut GetAll()
        {
            GetAllUserOut getAllUserOut = new GetAllUserOut();

            List<UserBE> listUserBE = _userRepository.GetAll();
            List<UserVM> listUserVM = _mapper.Map<List<UserVM>>(listUserBE);

            getAllUserOut.Result = MethodParameters.General.Result.Success;
            getAllUserOut.ListUser = listUserVM.OrderByDescending(x => x.Id).ToList();

            return getAllUserOut;
        }

        public UpdateUserOut Delete(UpdateUserIn updateUserIn)
        {
            UpdateUserOut updateUserOut = new UpdateUserOut();
            bool result = _userRepository.Delete(_mapper.Map<UserBE>(updateUserIn.User));
            updateUserOut.Result = result ? MethodParameters.General.Result.Success : MethodParameters.General.Result.Error;
            return updateUserOut;
        }
    }

}
