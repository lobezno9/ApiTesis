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
    public class AppSettingRepository : IAppSettingRepository
    {
        ProjectContext _context;
        #region ctor
        public AppSettingRepository(DbContext context) => this._context = (ProjectContext)context;
        #endregion

        public List<AppSettingBE> GetAll(AppSettingBE appSettingBE)
        {
            using (DbCommand command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = StoredProcedureName.spr_AppSettings_SelectCommand;

                if (command.Connection.State == System.Data.ConnectionState.Closed)
                    command.Connection.Open();

                #region Parameters
                command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.Id, GenericMethods.SetDefaultParameterValue(appSettingBE.Id)));
                command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.Key, GenericMethods.SetDefaultParameterValue(appSettingBE.Key)));
                command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.Value, GenericMethods.SetDefaultParameterValue(appSettingBE.Value)));
                command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.Group, GenericMethods.SetDefaultParameterValue(appSettingBE.Group)));
                #endregion

                DbDataReader res = command.ExecuteReader();
                List<AppSettingBE> listAppSettingBE = new List<AppSettingBE>();
                AppSettingBE itemAppSettingBE;
                if (res.HasRows)
                {
                    while (res.Read())
                    {
                        itemAppSettingBE = new AppSettingBE();
                        itemAppSettingBE.Id = !res.IsDBNull(0) ? res.GetInt32(0) : 0;
                        itemAppSettingBE.Key = !res.IsDBNull(1) ? res.GetString(1) : string.Empty;
                        itemAppSettingBE.Value = !res.IsDBNull(2) ? res.GetString(2) : string.Empty;
                        itemAppSettingBE.Group = !res.IsDBNull(3) ? res.GetString(3) : string.Empty;

                        listAppSettingBE.Add(itemAppSettingBE);
                    }
                }
                command.Connection.Close();

                return listAppSettingBE;
            }

        }
    }
}
