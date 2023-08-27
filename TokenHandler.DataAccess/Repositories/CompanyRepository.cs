using TokenHandler.Data.Context;
using TokenHandler.DataAccess.General;
using TokenHandler.DataAccess.Interface;
using TokenHandler.DataAccess.Resources;
using TokenHandler.Entities.BE;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace TokenHandler.DataAccess.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        TokenHandlerContext _context;
        #region ctor
        public CompanyRepository(DbContext context) => this._context = (TokenHandlerContext)context;
        #endregion

        public CompanyBE GetById(int companyId)
        {
            try
            {
                using (DbCommand command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureName.spr_Companies_SelectCommand;

                    if (command.Connection.State == System.Data.ConnectionState.Closed)
                        command.Connection.Open();

                    #region Parameters
                    command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.Id, GenericMethods.SetDefaultParameterValue(companyId)));
                    #endregion

                    DbDataReader res = command.ExecuteReader();

                    CompanyBE itemCompanyBE = new CompanyBE();
                    if (res.HasRows)
                    {
                        while (res.Read())
                        {
                            itemCompanyBE.Id = !res.IsDBNull(0) ? res.GetInt32(0) : 0;
                            itemCompanyBE.Name = !res.IsDBNull(1) ? res.GetString(1) : string.Empty;
                            itemCompanyBE.DatabaseName = !res.IsDBNull(2) ? res.GetString(2) : string.Empty;
                            itemCompanyBE.IsActive = !res.IsDBNull(3) ? res.GetBoolean(3) : false;

                        }
                    }
                    command.Connection.Close();

                    return itemCompanyBE;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
