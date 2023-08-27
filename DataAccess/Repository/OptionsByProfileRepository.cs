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
    public class OptionsByProfileRepository : IOptionsByProfileRepository
    {
        ProjectContext _context;

        #region ctor
        public OptionsByProfileRepository(DbContext context) => this._context = (ProjectContext)context;

        #endregion

        #region Public Method's

        /// <summary>
        /// Returns the register from tbl_OptionsByProfile by parameter filter or all registers.
        /// </summary>
        /// <param name="OptionsByProfileBE">DataBase model entity</param>
        /// <returns>List of OptionsByProfileBE</returns>
        public List<OptionsByProfileBE> GetAll(OptionsByProfileBE optionsByProfileBE)
        {
            try
            {
                using (DbCommand command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureName.spr_OptionsByProfile_SelectCommand;

                    if (command.Connection.State == System.Data.ConnectionState.Closed)
                        command.Connection.Open();

                    #region Parameters
                    MappOptionsByProfileBEToParameters(optionsByProfileBE, command);
                    #endregion

                    DbDataReader res = command.ExecuteReader();
                    List<OptionsByProfileBE> listOptionsByProfile = new List<OptionsByProfileBE>();
                    OptionsByProfileBE itemOptionsByProfileBE;
                    if (res.HasRows)
                    {
                        while (res.Read())
                        {
                            itemOptionsByProfileBE = new OptionsByProfileBE();
                            itemOptionsByProfileBE.Id = !res.IsDBNull(0) ? res.GetInt32(0) : 0;
                            itemOptionsByProfileBE.IdProfile = !res.IsDBNull(1) ? res.GetInt32(1) : 0;
                            itemOptionsByProfileBE.IdOption = !res.IsDBNull(2) ? res.GetInt32(2) : 0;
                            listOptionsByProfile.Add(itemOptionsByProfileBE);
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
        /// <param name="OptionsByProfileBE">DataBase model entity</param>
        /// <returns>Id from register add in tbl_OptionsByProfile.</returns>
        public int Add(OptionsByProfileBE optionsByProfileBE)
        {
            try
            {

                using (DbCommand command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureName.spr_OptionsByProfile_InsertCommand;

                    if (command.Connection.State == System.Data.ConnectionState.Closed)
                        command.Connection.Open();

                    #region Parameters
                    MappOptionsByProfileBEToParameters(optionsByProfileBE, command);
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
        /// <param name="optionsByProfileBE">DataBase model entity</param>
        /// <returns>1 if the process was complete succesfully</returns>
        public bool Delete(OptionsByProfileBE optionsByProfileBE)
        {
            try
            {

                using (DbCommand command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureName.spr_OptionsByProfile_DeleteCommand;

                    if (command.Connection.State == System.Data.ConnectionState.Closed)
                        command.Connection.Open();

                    #region Parameters
                    MappOptionsByProfileBEToParameters(optionsByProfileBE, command);
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
        private static void MappOptionsByProfileBEToParameters(OptionsByProfileBE optionsByProfileBE, DbCommand command)
        {
            command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.IdOption, GenericMethods.SetDefaultParameterValue(optionsByProfileBE.IdOption)));
            command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.IdProfile, GenericMethods.SetDefaultParameterValue(optionsByProfileBE.IdProfile)));
        }

        #endregion

    }
}
