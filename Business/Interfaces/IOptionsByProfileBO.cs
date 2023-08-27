using System;
using System.Collections.Generic;
using System.Text;
using Entities.BE;
using MethodParameters.MP;

namespace Business.Interfaces
{
    public interface IOptionsByProfileBO
    {

        /// <summary>
        /// Returns the register from tbl_OptionsByProfile by parameter filter or all registers.
        /// </summary>
        /// <param name="OptionsByProfileBE">DataBase model entity</param>
        /// <returns>List of OptionsByProfileBE</returns>
        GetAllOptionsByProfileOut GetAll(GetAllOptionsByProfileIn getAllOptionsByProfileIn);

        /// <summary>
        /// Add a register to tbl_OptionsByProfile.
        /// </summary>
        /// <param name="OptionsByProfileBE">DataBase model entity</param>
        /// <returns>Id from register add in tbl_OptionsByProfile.</returns>
        int Add(OptionsByProfileBE optionsByProfileBE);

        /// <summary>
        /// Delete a register to tblOptionsByProfile.
        /// </summary>
        /// <param name="optionsByProfileBE">DataBase model entity</param>
        /// <returns>1 if the process was complete succesfully</returns>
        bool Delete(OptionsByProfileBE optionsByProfileBE);

    }
}
