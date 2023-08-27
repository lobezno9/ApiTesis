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
    public class OptionRepository : IOptionRepository
    {
        ProjectContext _context;

        #region ctor
        public OptionRepository(DbContext context) => this._context = (ProjectContext)context;
        #endregion

        #region Public Method's
        public List<OptionBE> GetAll(OptionBE OptionBE, int? profileId = null)
        {
            using (DbCommand command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = StoredProcedureName.spr_Options_SelectCommand;

                if (command.Connection.State == System.Data.ConnectionState.Closed)
                    command.Connection.Open();

                #region Parameters
                command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.Id, GenericMethods.SetDefaultParameterValue(OptionBE.Id)));
                command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.ProfileId, GenericMethods.SetDefaultParameterValue(profileId)));
                MappOptionBEToParameters(OptionBE, command);
                #endregion

                DbDataReader res = command.ExecuteReader();
                List<OptionBE> listuserBE = new List<OptionBE>();
                OptionBE itemOptionBE;
                if (res.HasRows)
                {
                    while (res.Read())
                    {
                        itemOptionBE = new OptionBE();
                        itemOptionBE.Id = !res.IsDBNull(0) ? res.GetInt32(0) : 0;
                        itemOptionBE.Url = !res.IsDBNull(1) ? res.GetString(1) : string.Empty;
                        itemOptionBE.Description = !res.IsDBNull(2) ? res.GetString(2) : string.Empty;
                        itemOptionBE.ParentId = !res.IsDBNull(3) ? res.GetInt32(3) : 0;
                        itemOptionBE.OrderMenu = !res.IsDBNull(4) ? res.GetInt32(4) : 0;
                        itemOptionBE.Token = !res.IsDBNull(5) ? res.GetString(5) : string.Empty;
                        itemOptionBE.Icon = !res.IsDBNull(6) ? res.GetString(6) : string.Empty;
                        itemOptionBE.Title = !res.IsDBNull(7) ? res.GetString(7) : string.Empty;

                        listuserBE.Add(itemOptionBE);
                    }
                }
                command.Connection.Close();

                return listuserBE;
            }
        }

        public int Add(OptionBE OptionBE)
        {
            using (DbCommand command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = StoredProcedureName.spr_Options_InsertCommand;

                if (command.Connection.State == System.Data.ConnectionState.Closed)
                    command.Connection.Open();

                #region Parameters
                MappOptionBEToParameters(OptionBE, command);
                #endregion

                int res = Convert.ToInt32(command.ExecuteScalar());
                command.Connection.Close();

                return res;
            }
        }

        public bool Update(OptionBE OptionBE)
        {
            using (DbCommand command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = StoredProcedureName.spr_Options_UpdateCommand;

                if (command.Connection.State == System.Data.ConnectionState.Closed)
                    command.Connection.Open();

                #region Parameters
                command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.Id, GenericMethods.SetDefaultParameterValue(OptionBE.Id)));
                MappOptionBEToParameters(OptionBE, command);
                #endregion

                bool res = Convert.ToBoolean(command.ExecuteScalar());
                command.Connection.Close();

                return res;
            }
        }

        public bool Remove(int id)
        {
            using (DbCommand command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = StoredProcedureName.spr_Options_DeleteCommand;

                if (command.Connection.State == System.Data.ConnectionState.Closed)
                    command.Connection.Open();

                #region Parameters
                command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.Id, GenericMethods.SetDefaultParameterValue(id)));
                #endregion

                bool res = Convert.ToBoolean(command.ExecuteScalar());
                command.Connection.Close();

                return res;
            }
        }
        #endregion

        #region Private Method's
        private static void MappOptionBEToParameters(OptionBE OptionBE, DbCommand command)
        {
            command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.Url, GenericMethods.SetDefaultParameterValue(OptionBE.Url)));
            command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.Description, GenericMethods.SetDefaultParameterValue(OptionBE.Description)));
            command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.ParentId, GenericMethods.SetDefaultParameterValue(OptionBE.ParentId)));
            command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.OrderMenu, GenericMethods.SetDefaultParameterValue(OptionBE.OrderMenu)));
            command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.Token, GenericMethods.SetDefaultParameterValue(OptionBE.Token)));
            command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.Icon, GenericMethods.SetDefaultParameterValue(OptionBE.Icon)));
            command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.Title, GenericMethods.SetDefaultParameterValue(OptionBE.Title)));
        }


        #endregion
    }
}
