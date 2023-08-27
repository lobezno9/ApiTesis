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
    public class ProfileBO : IProfileBO
    {

        IProfileRepository _profileRepository;
        IOptionsByProfileRepository _optionsByProfileRepository;
        IPermissionsOptionsByProfileRepository _PermissionsOptionsByProfileRepository;

        private readonly IMapper _mapper;
        public ProfileBO(IMapper mapper, ProjectContext context)
        {
            _profileRepository = new ProfileRepository(context);
            _optionsByProfileRepository = new OptionsByProfileRepository(context);
            _PermissionsOptionsByProfileRepository = new PermissionsOptionsByProfileRepository(context);
            _mapper = mapper;
        }

        /// <summary>
        /// Returns the register from tbl_Profiles by parameter filter or all registers.
        /// </summary>
        /// <param name="profileBE">DataBase model entity</param>
        /// <returns>List of ProfileBE</returns>
        public GetAllProfileOut GetAll(GetAllProfileIn getAllProfileIn)
        {
            try
            {
                GetAllProfileOut getAllProfileOut = new GetAllProfileOut();
                List<ProfileBE> listProfileBE = _profileRepository.GetAll(_mapper.Map<ProfileBE>(getAllProfileIn.Profile ?? new ProfileVM()));
                List<ProfileVM> listProfileVM = _mapper.Map<List<ProfileVM>>(listProfileBE);

                getAllProfileOut.Result = MethodParameters.General.Result.Success;
                getAllProfileOut.ListProfile = listProfileVM.OrderByDescending(x => x.Id).ToList();

                return getAllProfileOut;
            }
            catch (Exception ex)
            {
                return new GetAllProfileOut()
                {
                    Message = ex.Message,
                    Result = MethodParameters.General.Result.Error
                };
            }
        }

        /// <summary>
        /// Add a register to tbl_Profiles.
        /// </summary>
        /// <param name="profileBE">DataBase model entity</param>
        /// <returns>Id from register add in tbl_Profiles.</returns>
        public AddProfileOut Add(AddProfileIn addProfileIn)
        {
            try
            {
                AddProfileOut addProfileOut = new AddProfileOut();
                ProfileBE profileBE = new ProfileBE();
                addProfileIn.Profile.Description = addProfileIn.Profile.Description.ToUpper();
                profileBE.Id = _profileRepository.Add(_mapper.Map<ProfileBE>(addProfileIn.Profile ?? new ProfileVM()));

                if (addProfileIn.ListOption != null && addProfileIn.ListOption.Count > 0)
                {
                    //foreach (OptionVM itemOptionVM in addProfileIn.ListOption)
                    //{
                    //    _optionsByProfileRepository.Add(new OptionsByProfileBE()
                    //    {
                    //        IdOption = itemOptionVM.Id,
                    //        IdProfile = profileBE.Id
                    //    });
                    //}
                    addProfileIn.ListOption.ForEach(x => {
                        _optionsByProfileRepository.Add(new OptionsByProfileBE() { IdOption = x.Id, IdProfile = profileBE.Id });
                        x.ListPermission.Where(w => w.IsChecked == true).ToList().ForEach(p => _PermissionsOptionsByProfileRepository.Add(new PermissionsOptionsByProfileBE() { IdOption = x.Id, IdProfile = profileBE.Id, IdPermission = p.Id }));
                    });
                }   

                addProfileOut.Id = profileBE.Id;
                addProfileOut.Result = MethodParameters.General.Result.Success;
                addProfileOut.Id = profileBE.Id;
                return addProfileOut;
            }
            catch (Exception ex)
            {
                return new AddProfileOut()
                {
                    Message = ex.Message,
                    Result = MethodParameters.General.Result.Error
                };
            }
        }

        /// <summary>
        /// Update a register to tbl_Profiles.
        /// </summary>
        /// <param name="profileBE">DataBase model entity</param>
        /// <returns>1 if the process was complete succesfully</returns>
        public UpdateProfileOut Update(UpdateProfileIn updateProfileIn)
        {
            try
            {
                UpdateProfileOut updateProfileOut = new UpdateProfileOut();
                ProfileBE profileBE = new ProfileBE();
                updateProfileIn.Profile.Description = updateProfileIn.Profile.Description.ToUpper();
                _profileRepository.Update(_mapper.Map<ProfileBE>(updateProfileIn.Profile ?? new ProfileVM()));
                if (updateProfileIn.ListOption != null)
                {
                    //OptionsByProfileBE optionsByProfileBE = new OptionsByProfileBE() { IdProfile = updateProfileIn.Profile.Id };
                    _optionsByProfileRepository.Delete(new OptionsByProfileBE() { IdProfile = updateProfileIn.Profile.Id });
                    _PermissionsOptionsByProfileRepository.Delete(new PermissionsOptionsByProfileBE() { IdProfile = updateProfileIn.Profile.Id });
                    if (updateProfileIn.ListOption != null && updateProfileIn.ListOption.Count > 0)
                    {
                        //foreach (OptionVM itemOptionVM in updateProfileIn.ListOption)
                        //{
                        //    _optionsByProfileRepository.Add(new OptionsByProfileBE()
                        //    {
                        //        IdOption = itemOptionVM.Id,
                        //        IdProfile = updateProfileIn.Profile.Id
                        //    });
                        //}
                        updateProfileIn.ListOption.ForEach(x => {
                            _optionsByProfileRepository.Add(new OptionsByProfileBE() { IdOption = x.Id, IdProfile = updateProfileIn.Profile.Id });
                            x.ListPermission.Where(w => w.IsChecked == true).ToList().ForEach(p => _PermissionsOptionsByProfileRepository.Add(new PermissionsOptionsByProfileBE() { IdOption = x.Id, IdProfile = updateProfileIn.Profile.Id, IdPermission = p.Id }));
                        }
                        );

                        //_PermissionsOptionsByProfileRepository.Delete(new PermissionsOptionsByProfileBE() { IdProfile = updateProfileIn.Profile.Id });
                    }
                }
                updateProfileOut.Result = MethodParameters.General.Result.Success;

                return updateProfileOut;

            }
            catch (Exception ex)
            {
                return new UpdateProfileOut()
                {
                    Message = ex.Message,
                    Result = MethodParameters.General.Result.Error
                };
            }
        }

        public UpdateProfileOut Delete(UpdateProfileIn updateProfileIn)
        {
            UpdateProfileOut result = new UpdateProfileOut();

            bool respuestaPOP = _PermissionsOptionsByProfileRepository.Delete(new PermissionsOptionsByProfileBE() { IdProfile = updateProfileIn.Profile.Id } ?? new PermissionsOptionsByProfileBE());
            if (respuestaPOP == false)
            {
                result.Message = "Ocurrio un error, Borrando los permisos del perfil";
                result.Result = MethodParameters.General.Result.Error;
                return result;
            }

            bool respuestaOP = _optionsByProfileRepository.Delete(new OptionsByProfileBE() { IdProfile = updateProfileIn.Profile.Id } ?? new OptionsByProfileBE());
            if (respuestaOP == false)
            {
                result.Message = "Ocurrio un error, Borrando el menu del perfil";
                result.Result = MethodParameters.General.Result.Error;
                return result;
            }

            bool respuestP = _profileRepository.Delete(_mapper.Map<ProfileBE>(updateProfileIn.Profile ?? new ProfileVM()));
            if (respuestP == false)
            {
                result.Message = "Ocurrio un error, Borrando el  perfil";
                result.Result = MethodParameters.General.Result.Error;
                return result;
            }
            result.Result = MethodParameters.General.Result.Success;
            return result;
        }
    }

}
