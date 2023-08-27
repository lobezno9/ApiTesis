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
    public class ExceptionLogRepository : IExceptionLogRepository
    {
        TokenHandlerContext _context;
        #region ctor
        public ExceptionLogRepository(DbContext context) => this._context = (TokenHandlerContext)context;
        #endregion

        public void Add(ExceptionLogBE exceptionLogBE)
        {
            try
            {
                using (DbCommand command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureName.spr_ExceptionLog_InsertCommand;

                    if (command.Connection.State == System.Data.ConnectionState.Closed)
                        command.Connection.Open();

                    #region Parameters
                    command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.Date, GenericMethods.SetDefaultParameterValue(exceptionLogBE.Date)));
                    command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.Exception, GenericMethods.SetDefaultParameterValue(exceptionLogBE.Exception)));
                    command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.Method, GenericMethods.SetDefaultParameterValue(exceptionLogBE.Method)));
                    command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.Namespace, GenericMethods.SetDefaultParameterValue(exceptionLogBE.Namespace)));
                    command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.StackTrace, GenericMethods.SetDefaultParameterValue(exceptionLogBE.StackTrace)));
                    command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.CustomData, GenericMethods.SetDefaultParameterValue(exceptionLogBE.CustomData)));
                    #endregion

                    command.ExecuteNonQuery();
                    command.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }
    }
}
