using System;
using System.Collections.Generic;
using System.Text;
using MethodParameters.MP;

namespace Business.Interfaces
{
    public interface IProfileBO
    {

        /// <summary>
        /// Returns the register from tbl_Profiles by parameter filter or all registers.
        /// </summary>
        /// <param name="profileBE">DataBase model entity</param>
        /// <returns>List of ProfileBE</returns>
        GetAllProfileOut GetAll(GetAllProfileIn getAllProfileIn);

        /// <summary>
        /// Add a register to tbl_Profiles.
        /// </summary>
        /// <param name="profileBE">DataBase model entity</param>
        /// <returns>Id from register add in tbl_Profiles.</returns>
        AddProfileOut Add(AddProfileIn addProfileIn);

        /// <summary>
        /// Update a register to tbl_Profiles.
        /// </summary>
        /// <param name="profileBE">DataBase model entity</param>
        /// <returns>1 if the process was complete succesfully</returns>
        UpdateProfileOut Update(UpdateProfileIn updateProfileIn);
        UpdateProfileOut Delete(UpdateProfileIn updateProfileIn);
    }
}
