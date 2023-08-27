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
    public class OptionsByProfileBO : IOptionsByProfileBO
    {


        IOptionsByProfileRepository _optionsByProfileRepository;
        private readonly IMapper _mapper;
        public OptionsByProfileBO(IMapper mapper, ProjectContext context)
        {
            _optionsByProfileRepository = new OptionsByProfileRepository(context);
            _mapper = mapper;
        }


        /// <summary>
        /// Returns the register from tbl_OptionsByProfile by parameter filter or all registers.
        /// </summary>
        /// <param name="OptionsByProfileBE">DataBase model entity</param>
        /// <returns>List of OptionsByProfileBE</returns>
        public GetAllOptionsByProfileOut GetAll(GetAllOptionsByProfileIn getAllOptionsByProfileIn)
        {
            try
            {
                GetAllOptionsByProfileOut getAllOptionsByProfileOut = new GetAllOptionsByProfileOut();
                List<OptionsByProfileBE> listOptionsByProfileBE = _optionsByProfileRepository.GetAll(_mapper.Map<OptionsByProfileBE>(getAllOptionsByProfileIn.OptionsByProfile ?? new OptionsByProfileVM()));
                List<OptionsByProfileVM> listOptionsByProfileVM = _mapper.Map<List<OptionsByProfileVM>>(listOptionsByProfileBE);

                getAllOptionsByProfileOut.Result = MethodParameters.General.Result.Success;
                getAllOptionsByProfileOut.ListOptionsByProfileVM = listOptionsByProfileVM;

                return getAllOptionsByProfileOut;
            }
            catch (Exception ex)
            {
                return new GetAllOptionsByProfileOut()
                {
                    Message = ex.Message,
                    Result = MethodParameters.General.Result.Error
                };
            }
        }

        /// <summary>
        /// Add a register to tbl_OptionsByProfile.
        /// </summary>
        /// <param name="OptionsByProfileBE">DataBase model entity</param>
        /// <returns>Id from register add in tbl_OptionsByProfile.</returns>
        public int Add(OptionsByProfileBE optionsByProfileBE)
        {
            try
            {
                int result = _optionsByProfileRepository.Add(optionsByProfileBE);
                return result;

            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Delete a register to tblOptionsByProfile.
        /// </summary>
        /// <param name="optionsByProfileBE">DataBase model entity</param>
        /// <returns>1 if the process was complete succesfully</returns>
        public bool Delete(OptionsByProfileBE optionsByProfileBE)
        {
            try
            {
                bool result = _optionsByProfileRepository.Delete(optionsByProfileBE);
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }

}
