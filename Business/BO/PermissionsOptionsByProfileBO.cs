using AutoMapper;
using Business.Interfaces;
using Data.Context;
using DataAccess.Interfaces;
using DataAccess.Repository;
using Entities.BE;
using MethodParameters.MP;
using MethodParameters.VM;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.BO
{
    public class PermissionsOptionsByProfileBO : IPermissionsOptionsByProfileBO
    {
        IPermissionsOptionsByProfileRepository _permissionsOptionsByProfileBO;
        private readonly IMapper _mapper;
        public PermissionsOptionsByProfileBO(IMapper mapper, ProjectContext context)
        {
            _permissionsOptionsByProfileBO = new PermissionsOptionsByProfileRepository(context);
            _mapper = mapper;
        }


        /// <summary>
        /// Returns the register from tbl_PermissionsOptionsByProfile by parameter filter or all registers.
        /// </summary>
        /// <param name="PermissionsOptionsByProfileBE">DataBase model entity</param>
        /// <returns>List of PermissionsOptionsByProfileBE</returns>
        public GetPermissionsOptionsByProfileOut GetAll(GetPermissionsOptionsByProfileIn getPermissionsOptionsByProfileIn)
        {
            try
            {
                GetPermissionsOptionsByProfileOut getPermissionsOptionsByProfileOut = new GetPermissionsOptionsByProfileOut();
                List<PermissionsOptionsByProfileBE> listPermissionsOptionsByProfileBE = _permissionsOptionsByProfileBO.GetAll(_mapper.Map<PermissionsOptionsByProfileBE>(getPermissionsOptionsByProfileIn.PermissionsOptionsByProfile ?? new PermissionsOptionsByProfileVM()));
                List<PermissionsOptionsByProfileVM> listPermissionsOptionsByProfileVM = _mapper.Map<List<PermissionsOptionsByProfileVM>>(listPermissionsOptionsByProfileBE);

                getPermissionsOptionsByProfileOut.Result = MethodParameters.General.Result.Success;
                getPermissionsOptionsByProfileOut.listPermissionsOptionsByProfile = listPermissionsOptionsByProfileVM;

                return getPermissionsOptionsByProfileOut;
            }
            catch (Exception ex)
            {
                return new GetPermissionsOptionsByProfileOut()
                {
                    Message = ex.Message,
                    Result = MethodParameters.General.Result.Error
                };
            }
        }

        /// <summary>
        /// Add a register to tbl_PermissionsOptionsByProfile.
        /// </summary>
        /// <param name="PermissionsOptionsByProfileBE">DataBase model entity</param>
        /// <returns>Id from register add in tbl_PermissionsOptionsByProfile.</returns>
        public int Add(PermissionsOptionsByProfileBE permissionsOptionsByProfileBE)
        {
            try
            {
                int result = _permissionsOptionsByProfileBO.Add(permissionsOptionsByProfileBE);
                return result;

            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Delete a register to tblPermissionsOptionsByProfile.
        /// </summary>
        /// <param name="optionsByProfileBE">DataBase model entity</param>
        /// <returns>1 if the process was complete succesfully</returns>
        public bool Delete(PermissionsOptionsByProfileBE permissionsOptionsByProfileBE)
        {
            try
            {
                bool result = _permissionsOptionsByProfileBO.Delete(permissionsOptionsByProfileBE);
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
