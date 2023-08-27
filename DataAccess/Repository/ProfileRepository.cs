using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Interfaces;
using Entities.BE;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using DataAccess.Resources;
using DataAccess.General;


namespace DataAccess.Repository
{
    public class ProfileRepository : IProfileRepository
    {
        ProjectContext _context;

        #region ctor
        public ProfileRepository(DbContext context) => this._context = (ProjectContext)context;

        #endregion

        #region Public Method's

        /// <summary>
        /// Returns the register from tbl_Profiles by parameter filter or all registers.
        /// </summary>
        /// <param name="profileBE">DataBase model entity</param>
        /// <returns>List of ProfileBE</returns>
        public List<ProfileBE> GetAll(ProfileBE profileBE)
        {
            try
            {
                using (DbCommand command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureName.spr_Profiles_SelectCommand;

                    if (command.Connection.State == System.Data.ConnectionState.Closed)
                        command.Connection.Open();

                    #region Parameters
                    MappProfileBEToParameters(profileBE, command);
                    #endregion

                    DbDataReader res = command.ExecuteReader();
                    List<ProfileBE> listProfileBE = new List<ProfileBE>();
                    ProfileBE itemProfileBE;
                    if (res.HasRows)
                    {
                        while (res.Read())
                        {
                            itemProfileBE = new ProfileBE();
                            itemProfileBE.Id = !res.IsDBNull(0) ? res.GetInt32(0) : 0;
                            itemProfileBE.ProfileCode = !res.IsDBNull(1) ? res.GetString(1) : string.Empty;
                            itemProfileBE.Description = !res.IsDBNull(2) ? res.GetString(2) : string.Empty;
                            itemProfileBE.IsSuperAdmin = !res.IsDBNull(3) ? res.GetBoolean(3) : false;
                            listProfileBE.Add(itemProfileBE);
                        }
                    }
                    command.Connection.Close();

                    return listProfileBE;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Add a register to tbl_Profiles.
        /// </summary>
        /// <param name="profileBE">DataBase model entity</param>
        /// <returns>Id from register add in tbl_Profiles.</returns>
        public int Add(ProfileBE profileBE)
        {
            try
            {

                using (DbCommand command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureName.spr_Profiles_InsertCommand;

                    if (command.Connection.State == System.Data.ConnectionState.Closed)
                        command.Connection.Open();

                    #region Parameters
                    MappProfileBEToParameters(profileBE, command);
                    #endregion

                    int res = Convert.ToInt32(command.ExecuteScalar());
                    command.Connection.Close();

                    return res;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// Update a register to tbl_Profiles.
        /// </summary>
        /// <param name="profileBE">DataBase model entity</param>
        /// <returns>1 if the process was complete succesfully</returns>
        public int Update(ProfileBE profileBE)
        {
            try
            {

                using (DbCommand command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureName.spr_Profiles_UpdateCommand;

                    if (command.Connection.State == System.Data.ConnectionState.Closed)
                        command.Connection.Open();

                    #region Parameters
                    MappProfileBEToParameters(profileBE, command);
                    #endregion

                    int res = Convert.ToInt32(command.ExecuteScalar());
                    command.Connection.Close();

                    return res;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return 0;
            }
        }
        public bool Delete(ProfileBE profileBE)
        {
            using (DbCommand command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = StoredProcedureName.spr_Profiles_DeleteCommand;

                if (command.Connection.State == System.Data.ConnectionState.Closed)
                    command.Connection.Open();

                #region Parameters
                command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.Id, GenericMethods.SetDefaultParameterValue(profileBE.Id)));
                #endregion

                bool res = Convert.ToBoolean(command.ExecuteScalar());
                command.Connection.Close();

                return res;
            }
        }
        #endregion

        #region Private Method's
        private static void MappProfileBEToParameters(ProfileBE profileBE, DbCommand command)
        {
            command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.Id, GenericMethods.SetDefaultParameterValue(profileBE.Id)));
            command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.ProfileCode, GenericMethods.SetDefaultParameterValue(profileBE.ProfileCode)));
            command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.Description, GenericMethods.SetDefaultParameterValue(profileBE.Description)));
            command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.IsSuperAdmin, GenericMethods.SetDefaultParameterValue(profileBE.IsSuperAdmin)));
        }
        #endregion
    }
}