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
    public class PermissionsByOptionRepository : IPermissionsByOptionRepository
    {
        ProjectContext _context;

        #region ctor
        public PermissionsByOptionRepository(DbContext context) => this._context = (ProjectContext)context;

        #endregion

        #region Public Method's

        /// <summary>
        /// Returns the register from tbl_PermissionsByOptions by parameter filter or all registers.
        /// </summary>
        /// <param name="permissionsByOptionBE">DataBase model entity</param>
        /// <returns>List of PermissionsByOptionBE</returns>
        public List<PermissionsByOptionBE> GetAll(PermissionsByOptionBE permissionsByOptionBE)
        {
            try
            {
                using (DbCommand command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureName.spr_PermissionsByOptions_SelectCommand;

                    if (command.Connection.State == System.Data.ConnectionState.Closed)
                        command.Connection.Open();

                    #region Parameters
                    command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.Id, GenericMethods.SetDefaultParameterValue(permissionsByOptionBE.Id)));
                    MappPermissionsByOptionBEToParameters(permissionsByOptionBE, command);
                    #endregion

                    DbDataReader res = command.ExecuteReader();
                    List<PermissionsByOptionBE> listPermissionsByOptionBE = new List<PermissionsByOptionBE>();
                    PermissionsByOptionBE itemPermissionsByOptionBE;
                    if (res.HasRows)
                    {
                        while (res.Read())
                        {
                            itemPermissionsByOptionBE = new PermissionsByOptionBE();
                            itemPermissionsByOptionBE.Id = !res.IsDBNull(0) ? res.GetInt32(0) : 0;
                            itemPermissionsByOptionBE.IdPermission = !res.IsDBNull(1) ? res.GetInt32(1) :0;
                            itemPermissionsByOptionBE.IdOption = !res.IsDBNull(2) ? res.GetInt32(2) :0;

                            listPermissionsByOptionBE.Add(itemPermissionsByOptionBE);
                        }
                    }
                    command.Connection.Close();

                    return listPermissionsByOptionBE;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public int Add(PermissionsByOptionBE permissionsByOptionBE)
        {
            using (DbCommand command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = StoredProcedureName.spr_PermissionsByOptions_InsertCommand;

                if (command.Connection.State == System.Data.ConnectionState.Closed)
                    command.Connection.Open();

                #region Parameters
                MappPermissionsByOptionBEToParameters(permissionsByOptionBE, command);
                #endregion

                int res = Convert.ToInt32(command.ExecuteScalar());
                command.Connection.Close();

                return res;
            }
        }

        
        public bool Delete(PermissionsByOptionBE permissionsByOptionBE)
        {
            using (DbCommand command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = StoredProcedureName.spr_PermissionsByOptions_DeleteCommand;

                if (command.Connection.State == System.Data.ConnectionState.Closed)
                    command.Connection.Open();

                #region Parameters
                MappPermissionsByOptionBEToParameters(permissionsByOptionBE, command);
                #endregion
                bool res = Convert.ToBoolean(command.ExecuteScalar());
                command.Connection.Close();

                return res;
            }
        }
        #endregion

        #region Private Method's
        private static void MappPermissionsByOptionBEToParameters(PermissionsByOptionBE permissionsByOptionBE, DbCommand command)
        {
            command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.IdPermission, GenericMethods.SetDefaultParameterValue(permissionsByOptionBE.IdPermission)));
            command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.IdOption, GenericMethods.SetDefaultParameterValue(permissionsByOptionBE.IdOption)));
        }


        #endregion
    }
}