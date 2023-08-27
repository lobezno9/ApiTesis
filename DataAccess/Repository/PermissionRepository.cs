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
    public class PermissionRepository : IPermissionRepository
    {
        ProjectContext _context;

        #region ctor
        public PermissionRepository(DbContext context) => this._context = (ProjectContext)context;

        #endregion

        #region Public Method's

        /// <summary>
        /// Returns the register from tbl_Permissions by parameter filter or all registers.
        /// </summary>
        /// <param name="permissionBE">DataBase model entity</param>
        /// <returns>List of PermissionBE</returns>
        public List<PermissionBE> GetAll(PermissionBE permissionBE,int? id)
        {
            try
            {
                using (DbCommand command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureName.spr_Permissions_SelectCommand;

                    if (command.Connection.State == System.Data.ConnectionState.Closed)
                        command.Connection.Open();

                    #region Parameters
                    command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.Id, GenericMethods.SetDefaultParameterValue(permissionBE.Id)));
                    command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.IdOption, GenericMethods.SetDefaultParameterValue(id)));
                    MappPermissionBEToParameters(permissionBE, command);
                    #endregion

                    DbDataReader res = command.ExecuteReader();
                    List<PermissionBE> listPermissionBE = new List<PermissionBE>();
                    PermissionBE itemPermissionBE;
                    if (res.HasRows)
                    {
                        while (res.Read())
                        {
                            itemPermissionBE = new PermissionBE();
                            itemPermissionBE.Id = !res.IsDBNull(0) ? res.GetInt32(0) : 0;
                            itemPermissionBE.Description = !res.IsDBNull(1) ? res.GetString(1) : string.Empty;
                            listPermissionBE.Add(itemPermissionBE);
                        }
                    }
                    command.Connection.Close();

                    return listPermissionBE;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public int Add(PermissionBE permissionBE)
        {
            using (DbCommand command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = StoredProcedureName.spr_Permissions_InsertCommand;

                if (command.Connection.State == System.Data.ConnectionState.Closed)
                    command.Connection.Open();

                #region Parameters
                MappPermissionBEToParameters(permissionBE, command);
                #endregion

                int res = Convert.ToInt32(command.ExecuteScalar());
                command.Connection.Close();

                return res;
            }
        }

        public bool Update(PermissionBE permissionBE)
        {
            using (DbCommand command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = StoredProcedureName.spr_Permissions_UpdateCommand;

                if (command.Connection.State == System.Data.ConnectionState.Closed)
                    command.Connection.Open();

                #region Parameters
                command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.Id, GenericMethods.SetDefaultParameterValue(permissionBE.Id)));
                MappPermissionBEToParameters(permissionBE, command);
                #endregion
                bool res = Convert.ToBoolean(command.ExecuteScalar());
                command.Connection.Close();

                return res;
            }

        }

        public bool Delete(PermissionBE permissionBE)
        {
            using (DbCommand command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = StoredProcedureName.spr_Permissions_DeleteCommand;

                if (command.Connection.State == System.Data.ConnectionState.Closed)
                    command.Connection.Open();

                #region Parameters
                command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.Id, GenericMethods.SetDefaultParameterValue(permissionBE.Id)));
                #endregion
                bool res = Convert.ToBoolean(command.ExecuteScalar());
                command.Connection.Close();

                return res;
            }
        }
        #endregion

        #region Private Method's
        private static void MappPermissionBEToParameters(PermissionBE permissionBE, DbCommand command)
        {
            command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.Description, GenericMethods.SetDefaultParameterValue(permissionBE.Description)));
        }
        #endregion
    }
}