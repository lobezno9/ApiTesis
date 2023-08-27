using System;
using System.Collections.Generic;
using System.Text;
using Entities.BE;

namespace DataAccess.Interfaces
{
    public interface IProfileRepository
    {

        /// <summary>
        /// Returns the register from tbl_Profiles by parameter filter or all registers.
        /// </summary>
        /// <param name="profileBE">DataBase model entity</param>
        /// <returns>List of ProfileBE</returns>
        List<ProfileBE> GetAll(ProfileBE profileBE);

        /// <summary>
        /// Add a register to tbl_Profiles.
        /// </summary>
        /// <param name="profileBE">DataBase model entity</param>
        /// <returns>Id from register add in tbl_Profiles.</returns>
        int Add(ProfileBE profileBE);

        /// <summary>
        /// Update a register to tbl_Profiles.
        /// </summary>
        /// <param name="profileBE">DataBase model entity</param>
        /// <returns>1 if the process was complete succesfully</returns>
        int Update(ProfileBE profileBE);
        bool Delete(ProfileBE profileBE);
    }
}
