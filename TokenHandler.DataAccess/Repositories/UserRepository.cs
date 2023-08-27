using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TokenHandler.Data.Context;
using TokenHandler.DataAccess.General;
using TokenHandler.DataAccess.Interfaces;
using TokenHandler.DataAccess.Resources;
using TokenHandler.Entities.BE;

namespace TokenHandler.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        TokenHandlerContext _context;
        #region ctor
        public UserRepository(DbContext context) => this._context = (TokenHandlerContext)context;
        #endregion

        public UserBE GetById(int userId)
        {
            try
            {
                using (DbCommand command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureName.spr_Users_SelectCommand;

                    if (command.Connection.State == System.Data.ConnectionState.Closed)
                        command.Connection.Open();

                    #region Parameters
                    command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.Id, GenericMethods.SetDefaultParameterValue(userId)));
                    #endregion

                    DbDataReader res = command.ExecuteReader();

                    UserBE itemUserBE = new UserBE();
                    if (res.HasRows)
                    {
                        while (res.Read())
                        {
                            itemUserBE = new UserBE();
                            itemUserBE.Id = !res.IsDBNull(0) ? res.GetInt32(0) : 0;
                            itemUserBE.FirstName = !res.IsDBNull(1) ? res.GetString(1) : string.Empty;
                            itemUserBE.LastName = !res.IsDBNull(2) ? res.GetString(2) : string.Empty;
                            itemUserBE.Username = !res.IsDBNull(3) ? res.GetString(3) : string.Empty;
                            itemUserBE.Password = !res.IsDBNull(4) ? res.GetString(4) : string.Empty;
                            itemUserBE.CompanyId = !res.IsDBNull(5) ? res.GetInt32(5) : 0;
                            itemUserBE.ProfileId = !res.IsDBNull(6) ? res.GetInt32(6) : 0;
                            itemUserBE.IsActive = !res.IsDBNull(7) ? res.GetBoolean(7) : false;

                        }
                    }
                    command.Connection.Close();

                    return itemUserBE;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
