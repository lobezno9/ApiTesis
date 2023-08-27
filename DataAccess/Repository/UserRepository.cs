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
    public class UserRepository : IUserRepository
    {
        ProjectContext _context;

        #region ctor
        public UserRepository(DbContext context) => this._context = (ProjectContext)context;
        #endregion

        #region Public Method's
        public List<UserBE> GetAll(UserBE userBE)
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
                    command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.Id, GenericMethods.SetDefaultParameterValue(userBE.Id)));
                    MappUserBEToParameters(userBE, command);
                    #endregion

                    DbDataReader res = command.ExecuteReader();
                    List<UserBE> listuserBE = new List<UserBE>();
                    UserBE itemuserBE;
                    if (res.HasRows)
                    {
                        while (res.Read())
                        {
                            itemuserBE = new UserBE();
                            itemuserBE.Id = !res.IsDBNull(0) ? res.GetInt32(0) : 0;
                            itemuserBE.FirstName = !res.IsDBNull(1) ? res.GetString(1) : string.Empty;
                            itemuserBE.LastName = !res.IsDBNull(2) ? res.GetString(2) : string.Empty;
                            itemuserBE.Username = !res.IsDBNull(3) ? res.GetString(3) : string.Empty;
                            itemuserBE.Password = !res.IsDBNull(4) ? res.GetString(4) : string.Empty;
                            itemuserBE.CompanyId = !res.IsDBNull(5) ? res.GetInt32(5) : 0;
                            itemuserBE.ProfileId = !res.IsDBNull(6) ? res.GetInt32(6) : 0;
                            itemuserBE.IsActive = !res.IsDBNull(7) ? res.GetBoolean(7) : false;
                            itemuserBE.CreationDate = !res.IsDBNull(8) ? res.GetDateTime(8) : DateTime.MinValue;
                            itemuserBE.ModificationDate = !res.IsDBNull(9) ? res.GetDateTime(9) : DateTime.MinValue;
                            itemuserBE.Company = !res.IsDBNull(10) ? new CompanyBE() { Id = !res.IsDBNull(5) ? res.GetInt32(5) : 0, Name = res.GetString(10) } : null;
                            itemuserBE.Profile = !res.IsDBNull(11) ? new ProfileBE() { Id = !res.IsDBNull(6) ? res.GetInt32(6) : 0, Description = res.GetString(11) } : null;
                            itemuserBE.Email = !res.IsDBNull(12) ? res.GetString(12) : string.Empty;
                            itemuserBE.Identification = !res.IsDBNull(13) ? res.GetString(13) : string.Empty;
                            itemuserBE.UserTypeId = !res.IsDBNull(14) ? res.GetInt32(14) : 0;
                            itemuserBE.UserType = !res.IsDBNull(15) ? new UserTypeBE() { Id = !res.IsDBNull(15) ? res.GetInt32(14) : 0, Description = res.GetString(15) } : null;

                            listuserBE.Add(itemuserBE);
                        }
                    }
                    command.Connection.Close();

                    return listuserBE;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("test " + ex.Message);
                return null;
            }
        }
        public List<UserBE> GetAll()
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
                   
                    #endregion

                    DbDataReader res = command.ExecuteReader();
                    List<UserBE> listuserBE = new List<UserBE>();
                    UserBE itemuserBE;
                    if (res.HasRows)
                    {
                        while (res.Read())
                        {
                            itemuserBE = new UserBE();
                            itemuserBE.Id = !res.IsDBNull(0) ? res.GetInt32(0) : 0;
                            itemuserBE.FirstName = !res.IsDBNull(1) ? res.GetString(1) : string.Empty;
                            itemuserBE.LastName = !res.IsDBNull(2) ? res.GetString(2) : string.Empty;
                            itemuserBE.Username = !res.IsDBNull(3) ? res.GetString(3) : string.Empty;
                            itemuserBE.Password = !res.IsDBNull(4) ? res.GetString(4) : string.Empty;
                            itemuserBE.CompanyId = !res.IsDBNull(5) ? res.GetInt32(5) : 0;
                            itemuserBE.ProfileId = !res.IsDBNull(6) ? res.GetInt32(6) : 0;
                            itemuserBE.IsActive = !res.IsDBNull(7) ? res.GetBoolean(7) : false;
                            itemuserBE.CreationDate = !res.IsDBNull(8) ? res.GetDateTime(8) : DateTime.MinValue;
                            itemuserBE.ModificationDate = !res.IsDBNull(9) ? res.GetDateTime(9) : DateTime.MinValue;
                            itemuserBE.Company = !res.IsDBNull(10) ? new CompanyBE() { Id = !res.IsDBNull(5) ? res.GetInt32(5) : 0, Name = res.GetString(10) } : null;
                            itemuserBE.Profile = !res.IsDBNull(11) ? new ProfileBE() { Id = !res.IsDBNull(6) ? res.GetInt32(6) : 0, Description = res.GetString(11) } : null;
                            itemuserBE.Email = !res.IsDBNull(12) ? res.GetString(12) : string.Empty;
                            itemuserBE.Identification = !res.IsDBNull(13) ? res.GetString(13) : string.Empty;
                            itemuserBE.UserTypeId = !res.IsDBNull(14) ? res.GetInt32(14) : 0;

                            listuserBE.Add(itemuserBE);
                        }
                    }
                    command.Connection.Close();

                    return listuserBE;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("test " + ex.Message);
                return null;
            }
        }

        public int Add(UserBE userBE)
        {
            using (DbCommand command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = StoredProcedureName.spr_Users_InsertCommand;

                if (command.Connection.State == System.Data.ConnectionState.Closed)
                    command.Connection.Open();

                #region Parameters
                userBE.CreationDate = DateTime.Now;
                MappUserBEToParameters(userBE, command);
                #endregion

                int res = Convert.ToInt32(command.ExecuteScalar());
                command.Connection.Close();

                return res;
            }
        }

        public bool Update(UserBE userBE)
        {
            using (DbCommand command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = StoredProcedureName.spr_Users_UpdateCommand;

                if (command.Connection.State == System.Data.ConnectionState.Closed)
                    command.Connection.Open();

                #region Parameters
                command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.Id, GenericMethods.SetDefaultParameterValue(userBE.Id)));
                MappUserBEToParameters(userBE, command);
                #endregion

                bool res = Convert.ToBoolean(command.ExecuteScalar());
                command.Connection.Close();

                return res;
            }
        }

        /// <summary>
        /// Validates if the user name and the email match
        /// </summary>
        /// <param name="userBE">Get the username and email for execute the spr_Users_ValidateRecoverPasswordCommand</param>
        /// <returns>boolean value with true if the username and email match</returns>
        public bool ValidateRecoverPassword(UserBE userBE)
        {
            using (DbCommand command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = StoredProcedureName.spr_Users_ValidateRecoverPasswordCommand;

                if (command.Connection.State == System.Data.ConnectionState.Closed)
                    command.Connection.Open();

                #region Parameters
                command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.Username, GenericMethods.SetDefaultParameterValue(userBE.Username)));
                command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.Email, GenericMethods.SetDefaultParameterValue(userBE.Email)));
                #endregion

                bool res = Convert.ToBoolean(command.ExecuteScalar());
                command.Connection.Close();

                return res;
            }
        }
        public bool Delete(UserBE userBE)
        {
            using (DbCommand command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = StoredProcedureName.spr_Users_DeleteCommand;

                if (command.Connection.State == System.Data.ConnectionState.Closed)
                    command.Connection.Open();

                #region Parameters
                command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.Id, GenericMethods.SetDefaultParameterValue(userBE.Id)));
                #endregion

                bool res = Convert.ToBoolean(command.ExecuteScalar());
                command.Connection.Close();

                return res;
            }
        }
        #endregion

        #region Private Method's
        private static void MappUserBEToParameters(UserBE userBE, DbCommand command)
        {
            command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.FirstName, GenericMethods.SetDefaultParameterValue(userBE.FirstName)));
            command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.LastName, GenericMethods.SetDefaultParameterValue(userBE.LastName)));
            command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.Username, GenericMethods.SetDefaultParameterValue(userBE.Username)));
            command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.Email, GenericMethods.SetDefaultParameterValue(userBE.Email)));
            command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.Password, GenericMethods.SetDefaultParameterValue(userBE.Password)));
            command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.CompanyId, GenericMethods.SetDefaultParameterValue(userBE.CompanyId)));
            command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.ProfileId, GenericMethods.SetDefaultParameterValue(userBE.ProfileId)));
            command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.IsActive, GenericMethods.SetDefaultParameterValue(userBE.IsActive)));
            command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.CreationDate, GenericMethods.SetDefaultParameterValue(userBE.CreationDate)));
            command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.ModificationDate, GenericMethods.SetDefaultParameterValue(userBE.ModificationDate)));
            command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.Identification, GenericMethods.SetDefaultParameterValue(userBE.Identification)));
            command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.UserTypeId, GenericMethods.SetDefaultParameterValue(userBE.UserTypeId)));
        }

      
        #endregion
    }
}
