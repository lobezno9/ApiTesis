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
    public class PermissionsOptionsByProfileRepository : IPermissionsOptionsByProfileRepository
    {
        ProjectContext _context;

        #region ctor
        public PermissionsOptionsByProfileRepository(DbContext context) => this._context = (ProjectContext)context;

        #endregion

        #region Public Method's

        /// <summary>
        /// Returns the register from tbl_OptionsByProfile by parameter filter or all registers.
        /// </summary>
        /// <param name="PermissionsOptionsByProfileBE">DataBase model entity</param>
        /// <returns>List of PermissionsOptionsByProfileBE</returns>
        public List<PermissionsOptionsByProfileBE> GetAll(PermissionsOptionsByProfileBE permissionsOptionsByProfileBE)
        {
            try
            {
                using (DbCommand command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureName.spr_PermissionsOptionsByProfile_SelectCommand;

                    if (command.Connection.State == System.Data.ConnectionState.Closed)
                        command.Connection.Open();

                    #region Parameters
                    MappPermissionsOptionsByProfileBEToParameters(permissionsOptionsByProfileBE, command);
                    #endregion

                    DbDataReader res = command.ExecuteReader();
                    List<PermissionsOptionsByProfileBE> listOptionsByProfile = new List<PermissionsOptionsByProfileBE>();
                    PermissionsOptionsByProfileBE itemPermissionsOptionsByProfileBE;
                    if (res.HasRows)
                    {
                        while (res.Read())
                        {
                            itemPermissionsOptionsByProfileBE = new PermissionsOptionsByProfileBE();
                            itemPermissionsOptionsByProfileBE.Id = !res.IsDBNull(0) ? res.GetInt32(0) : 0;
                            itemPermissionsOptionsByProfileBE.IdProfile = !res.IsDBNull(1) ? res.GetInt32(1) : 0;
                            itemPermissionsOptionsByProfileBE.IdOption = !res.IsDBNull(2) ? res.GetInt32(2) : 0;
                            itemPermissionsOptionsByProfileBE.IdPermission = !res.IsDBNull(3) ? res.GetInt32(3) : 0;
                            itemPermissionsOptionsByProfileBE.IsActive = !res.IsDBNull(4) ? res.GetBoolean(4) : false;
                            itemPermissionsOptionsByProfileBE.Description = !res.IsDBNull(5) ? res.GetString(5) : string.Empty;
                            listOptionsByProfile.Add(itemPermissionsOptionsByProfileBE);
                        }
                    }
                    command.Connection.Close();

                    return listOptionsByProfile;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }
        /// <summary>
        /// Add a register to tbl_OptionsByProfile.
        /// </summary>
        /// <param name="PermissionsOptionsByProfileBE">DataBase model entity</param>
        /// <returns>Id from register add in tbl_OptionsByProfile.</returns>
        public int Add(PermissionsOptionsByProfileBE permissionsOptionsByProfileBE)
        {
            try
            {

                using (DbCommand command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureName.spr_PermissionsOptionsByProfile_InsertCommand;

                    if (command.Connection.State == System.Data.ConnectionState.Closed)
                        command.Connection.Open();

                    #region Parameters
                    MappPermissionsOptionsByProfileBEToParameters(permissionsOptionsByProfileBE, command);
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
        /// Delete a register to tblOptionsByProfile.
        /// </summary>
        /// <param name="permissionsOptionsByProfileBE">DataBase model entity</param>
        /// <returns>1 if the process was complete succesfully</returns>
        public bool Delete(PermissionsOptionsByProfileBE permissionsOptionsByProfileBE)
        {
            try
            {

                using (DbCommand command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureName.spr_PermissionsOptionsByProfile_DeleteCommand;

                    if (command.Connection.State == System.Data.ConnectionState.Closed)
                        command.Connection.Open();

                    #region Parameters
                    MappPermissionsOptionsByProfileBEToParameters(permissionsOptionsByProfileBE, command);
                    #endregion

                    bool res = Convert.ToBoolean(command.ExecuteScalar());

                    command.Connection.Close();

                    return res;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        #endregion

        #region Private Method's
        private static void MappPermissionsOptionsByProfileBEToParameters(PermissionsOptionsByProfileBE permissionsOptionsByProfileBE, DbCommand command)
        {
            command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.IdOption, GenericMethods.SetDefaultParameterValue(permissionsOptionsByProfileBE.IdOption)));
            command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.IdProfile, GenericMethods.SetDefaultParameterValue(permissionsOptionsByProfileBE.IdProfile)));
            command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.IdPermission, GenericMethods.SetDefaultParameterValue(permissionsOptionsByProfileBE.IdPermission)));
        }

        #endregion

    }
}