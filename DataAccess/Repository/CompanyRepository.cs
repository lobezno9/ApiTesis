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
    public class CompanyRepository : ICompanyRepository
    {
        ProjectContext _context;

        #region ctor
        public CompanyRepository(DbContext context) => this._context = (ProjectContext)context;
        #endregion

        #region Public Method's
        public List<CompanyBE> GetAll(CompanyBE companyBE)
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
                    command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.Id, GenericMethods.SetDefaultParameterValue(companyBE.Id)));
                    MappCompanyBEToParameters(companyBE, command);
                    #endregion

                    DbDataReader res = command.ExecuteReader();
                    List<CompanyBE> listuserBE = new List<CompanyBE>();
                    CompanyBE itemCompanyBE;
                    if (res.HasRows)
                    {
                        while (res.Read())
                        {
                            itemCompanyBE = new CompanyBE();
                            itemCompanyBE.Id = !res.IsDBNull(0) ? res.GetInt32(0) : 0;
                            itemCompanyBE.Name = !res.IsDBNull(1) ? res.GetString(1) : string.Empty;
                            itemCompanyBE.DatabaseName = !res.IsDBNull(2) ? res.GetString(2) : string.Empty;
                            itemCompanyBE.IsActive = !res.IsDBNull(3) ? res.GetBoolean(3) : false;
                            itemCompanyBE.CreationDate = !res.IsDBNull(4) ? res.GetDateTime(4) : DateTime.MinValue;
                            itemCompanyBE.ModificationDate = !res.IsDBNull(5) ? res.GetDateTime(5) : DateTime.MinValue;
                            itemCompanyBE.ImageLogo = !res.IsDBNull(6) ? (byte[])res[6] : null;
                            listuserBE.Add(itemCompanyBE);
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

        public int Add(CompanyBE companyBE)
        {
            using (DbCommand command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = StoredProcedureName.spr_Companies_InsertCommand;

                if (command.Connection.State == System.Data.ConnectionState.Closed)
                    command.Connection.Open();

                #region Parameters
                companyBE.IsActive = false;
                MappCompanyBEToParameters(companyBE, command);
                command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.ImageLogo, GenericMethods.SetDefaultParameterValue(companyBE.ImageLogo)));
                #endregion

                int res = Convert.ToInt32(command.ExecuteScalar());
                command.Connection.Close();

                return res;
            }
        }

        public bool Update(CompanyBE companyBE)
        {
            using (DbCommand command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = StoredProcedureName.spr_Companies_UpdateCommand;

                if (command.Connection.State == System.Data.ConnectionState.Closed)
                    command.Connection.Open();

                #region Parameters
                command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.Id, GenericMethods.SetDefaultParameterValue(companyBE.Id)));
                command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.ImageLogo, GenericMethods.SetDefaultParameterValue(companyBE.ImageLogo)));
                MappCompanyBEToParameters(companyBE, command);
                #endregion

                bool res = Convert.ToBoolean(command.ExecuteScalar());
                command.Connection.Close();

                return res;
            }
        }
        #endregion

        #region Private Method's
        private static void MappCompanyBEToParameters(CompanyBE companyBE, DbCommand command)
        {
            command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.Name, GenericMethods.SetDefaultParameterValue(companyBE.Name)));
            command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.DatabaseName, GenericMethods.SetDefaultParameterValue(companyBE.DatabaseName)));
            command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.IsActive, GenericMethods.SetDefaultParameterValue(companyBE.IsActive)));
            command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.CreationDate, GenericMethods.SetDefaultParameterValue(companyBE.CreationDate)));
            command.Parameters.Add(GenericMethods.CreateParameter(StoredProcedureParameterName.ModificationDate, GenericMethods.SetDefaultParameterValue(companyBE.ModificationDate)));

        }
        #endregion
    }
}
