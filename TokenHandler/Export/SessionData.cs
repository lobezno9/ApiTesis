using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using TokenHandler.Business.BO;
using TokenHandler.Business.Interfaces;
using TokenHandler.Data.Context;
using TokenHandler.Entities.BE;

namespace TokenHandler.Export
{
    public class SessionData
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionData(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public DynamicContext GetDymanicConnectionString()
        {
            try
            {
                string connectionString = string.Empty;
                IEnumerable<Claim> listClaim = _httpContextAccessor.HttpContext.User.Claims;
                if (listClaim != null)
                {
                    if (listClaim.Any(x => x.Type == "CompanyId"))
                    {
                        int companyId = Convert.ToInt32(listClaim.First(x => x.Type == "CompanyId").Value);

                        IConfiguration configuration = _httpContextAccessor.HttpContext.RequestServices.GetService(typeof(IConfiguration)) as IConfiguration;

                        if (!string.IsNullOrEmpty(configuration.GetConnectionString("ProjectContext")))
                        {
                            TokenHandlerContext _context = new TokenHandlerContext(configuration.GetConnectionString("ProjectContext"));
                            ICompanyBO companyBO = new CompanyBO(_context);
                            CompanyBE companyBE = companyBO.GetById(companyId);
                            connectionString = string.Format(configuration.GetConnectionString("ProjectContext"), companyBE.DatabaseName);
                        }
                    }
                }
                if (!string.IsNullOrEmpty(connectionString))
                {
                    return new DynamicContext(connectionString);
                }

                throw new Exception("Connection string null or empty");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public CustomUserIdentity GetUserIdentity()
        {
            try
            {
                IEnumerable<Claim> listClaim = _httpContextAccessor.HttpContext.User.Claims;
                CustomUserIdentity customUserIdentity = new CustomUserIdentity();
                if (listClaim != null && listClaim.Any())
                {
                    customUserIdentity.CompanyId = listClaim.Any(x => x.Type == "CompanyId") ? Convert.ToInt32(listClaim.First(x => x.Type == "CompanyId").Value) : 0;
                    customUserIdentity.ProfileId = listClaim.Any(x => x.Type == "ProfileId") ? Convert.ToInt32(listClaim.First(x => x.Type == "ProfileId").Value) : 0;
                    customUserIdentity.UserId = listClaim.Any(x => x.Type == "UserId") ? Convert.ToInt32(listClaim.First(x => x.Type == "UserId").Value) : 0;
                    customUserIdentity.IsSuperAdmin = listClaim.Any(x => x.Type == "IsSuperAdmin") ? Convert.ToBoolean(listClaim.First(x => x.Type == "IsSuperAdmin").Value) : false;
                    customUserIdentity.FullName = listClaim.Any(x => x.Type == "FullName") ? listClaim.First(x => x.Type == "FullName").Value : string.Empty;
                }
                return customUserIdentity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

    }
}
