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

namespace Business.BO
{
    public class UserTypeBO : IUserTypeBO
    {
        IUserTypeRepository _UserTypeRepository;
        private readonly IMapper _mapper;
        public UserTypeBO(IMapper mapper, ProjectContext context)
        {
            _UserTypeRepository = new UserTypeRepository(context);
            _mapper = mapper;
        }
        public GetUserTypeOut GetAll(GetUserTypeIn getUserTypeIn)
        {


            GetUserTypeOut getAllUserTypeOut = new GetUserTypeOut();
            List<UserTypeBE> listUserTypeBE = _UserTypeRepository.GetAll(_mapper.Map<UserTypeBE>(getUserTypeIn.UserType ?? new UserTypeVM()));
            List<UserTypeVM> listUserTypeVM = _mapper.Map<List<UserTypeVM>>(listUserTypeBE);

            getAllUserTypeOut.Result = MethodParameters.General.Result.Success;

            getAllUserTypeOut.ListUserType = listUserTypeVM;
            return getAllUserTypeOut;
        }

        public AddUserTypeOut Add(AddUserTypeIn addUserTypeIn)
        {
            AddUserTypeOut addUserTypeOut = new AddUserTypeOut();
            addUserTypeIn.UserType.Description = addUserTypeIn.UserType.Description.ToUpper();
            int newId = _UserTypeRepository.Add(_mapper.Map<UserTypeBE>(addUserTypeIn.UserType));
            addUserTypeOut.Result = newId > 0 ? MethodParameters.General.Result.Success : MethodParameters.General.Result.Error;
            addUserTypeOut.Id = newId;

            return addUserTypeOut;
        }

        public UpdateUserTypeOut Update(UpdateUserTypeIn updateUserTypeIn)
        {
            UpdateUserTypeOut updateUserTypeOut = new UpdateUserTypeOut();
            updateUserTypeIn.UserType.Description = updateUserTypeIn.UserType.Description.ToUpper();
            bool result = _UserTypeRepository.Update(_mapper.Map<UserTypeBE>(updateUserTypeIn.UserType));
            updateUserTypeOut.Result = result ? MethodParameters.General.Result.Success : MethodParameters.General.Result.Error;

            return updateUserTypeOut;
        }

        public UpdateUserTypeOut Delete(UpdateUserTypeIn updateUserTypeIn)
        {
            UpdateUserTypeOut updateUserTypeOut = new UpdateUserTypeOut();
            bool result = _UserTypeRepository.Delete(_mapper.Map<UserTypeBE>(updateUserTypeIn.UserType));
            updateUserTypeOut.Result = result ? MethodParameters.General.Result.Success : MethodParameters.General.Result.Error;

            return updateUserTypeOut;
        }
    }
}
