using Data.Context;
using DataAccess.General;
using DataAccess.Interfaces;
using DataAccess.Resources;
using Entities.BE;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace DataAccess.Repository
{
    public class UserTypeRepository : IUserTypeRepository
    {
        ProjectContext _context;

        #region ctor
        public UserTypeRepository(DbContext context) => this._context = (ProjectContext)context;

        #endregion

        #region Public Method's

        /// <summary>
        /// Returns the register from tbl_UserTypes by parameter filter or all registers.
        /// </summary>
        /// <param name="UserTypeBE">DataBase model entity</param>
        /// <returns>List of UserTypeBE</returns>
        public List<UserTypeBE> GetAll(UserTypeBE UserTypeBE)
        {
            try
            {
                using (DbCommand command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = Resources.StoredProcedureName.spr_UsersTypes_SelectCommand;

                    if (command.Connection.State == System.Data.ConnectionState.Closed)
                        command.Connection.Open();

                    #region Parameters
                    command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.Id, GenericMethods.SetDefaultParameterValue(UserTypeBE.Id)));
                    MappUserTypeBEToParameters(UserTypeBE, command);
                    #endregion

                    DbDataReader res = command.ExecuteReader();
                    List<UserTypeBE> listUserTypeBE = new List<UserTypeBE>();
                    UserTypeBE itemUserTypeBE;
                    if (res.HasRows)
                    {
                        while (res.Read())
                        {
                            itemUserTypeBE = new UserTypeBE();
                            itemUserTypeBE.Id = !res.IsDBNull(0) ? res.GetInt32(0) : 0;
                            itemUserTypeBE.Description = !res.IsDBNull(1) ? res.GetString(1) : string.Empty;
                            listUserTypeBE.Add(itemUserTypeBE);
                        }
                    }
                    command.Connection.Close();

                    return listUserTypeBE;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public int Add(UserTypeBE UserTypeBE)
        {
            using (DbCommand command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = StoredProcedureName.spr_UsersTypes_InsertCommand;

                if (command.Connection.State == System.Data.ConnectionState.Closed)
                    command.Connection.Open();

                #region Parameters
                MappUserTypeBEToParameters(UserTypeBE, command);
                #endregion

                int res = Convert.ToInt32(command.ExecuteScalar());
                command.Connection.Close();

                return res;
            }
        }

        public bool Update(UserTypeBE UserTypeBE)
        {
            using (DbCommand command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = StoredProcedureName.spr_UsersTypes_UpdateCommand;

                if (command.Connection.State == System.Data.ConnectionState.Closed)
                    command.Connection.Open();

                #region Parameters
                command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.Id, GenericMethods.SetDefaultParameterValue(UserTypeBE.Id)));
                MappUserTypeBEToParameters(UserTypeBE, command);
                #endregion
                bool res = Convert.ToBoolean(command.ExecuteScalar());
                command.Connection.Close();

                return res;
            }

        }
        public bool Delete(UserTypeBE userTypeBE)
        {
            using (DbCommand command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = StoredProcedureName.spr_UsersTypes_DeleteCommand;

                if (command.Connection.State == System.Data.ConnectionState.Closed)
                    command.Connection.Open();

                #region Parameters
                command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.Id, GenericMethods.SetDefaultParameterValue(userTypeBE.Id)));
                #endregion
                bool res = Convert.ToBoolean(command.ExecuteScalar());
                command.Connection.Close();

                return res;
            }
        }
        #endregion

        #region Private Method's
        private static void MappUserTypeBEToParameters(UserTypeBE UserTypeBE, DbCommand command)
        {
            command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.Description, GenericMethods.SetDefaultParameterValue(UserTypeBE.Description)));
        }


        #endregion
    }
}
