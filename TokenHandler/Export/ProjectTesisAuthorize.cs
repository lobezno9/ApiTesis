using TokenHandler.Business.BO;
using TokenHandler.Business.Interfaces;
using TokenHandler.Data.Context;
using TokenHandler.Entities.BE;
using TokenHandler.Entities.VM;
using TokenHandler.Interfaces;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using TokenHandler.Entities.General;
//using Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext.HttpContext.RequestServices.GetService;

namespace TokenHandler.Export
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ProjectTesisAuthorize : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            if (filterContext != null)
            {
                if (filterContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    var claimsIndentity = filterContext.HttpContext.User.Identity as ClaimsIdentity;
                    if (claimsIndentity != null)
                    {
                        IList<Claim> listClaim = claimsIndentity.Claims.ToList();
                        IConfiguration configuration = filterContext.HttpContext.RequestServices.GetService(typeof(IConfiguration)) as IConfiguration;

                        if (!string.IsNullOrEmpty(configuration.GetConnectionString("ProjectContext")))
                        {
                            TokenHandlerContext context = new TokenHandlerContext(configuration.GetConnectionString("ProjectContext"));
                            IAppSettingBO iAppSettingBO = new AppSettingBO(context);
                            Dictionary<string, string> appSetting = iAppSettingBO.GetAllByGroup("JWT");
                            if (appSetting != null)
                            {
                                if (!listClaim.Select(x => x.Issuer).Any(x => x.ToString().Equals(appSetting["Issuer"])) || (!listClaim.Any(x => x.Type == "CompanyId") || !listClaim.Any(x => x.Type == "UserId")))
                                {
                                    Unauthorized(filterContext, "Issuer error");
                                    return;
                                }
                                else if (listClaim.Any(x => x.Type == "CompanyId") && listClaim.Any(x => x.Type == "UserId"))
                                {
                                    ITokenValidateBO tokenValidateBO = new TokenValidateBO(context);
                                    TokenValidateVM validateUserInformationActive = tokenValidateBO.IsActiveUser(Convert.ToInt32(listClaim.First(x => x.Type == "CompanyId").Value), Convert.ToInt32(listClaim.First(x => x.Type == "UserId").Value));

                                    if (!validateUserInformationActive.IsActiveCompany)
                                    {
                                        Unauthorized(filterContext, "Company inactive");
                                        return;
                                    }

                                    if (!validateUserInformationActive.IsActiveUser)
                                    {
                                        Unauthorized(filterContext, "User inactive");
                                        return;
                                    }
                                }
                            }
                        }
                    }

                    filterContext.HttpContext.Response.Headers.Add("AuthStatus", "Authorized");
                    filterContext.HttpContext.Response.Headers.Add("storeAccessiblity", "Authorized");

                    return;
                }
                else
                {
                    Unauthorized(filterContext);
                }
            }
        }

        private static void Unauthorized(AuthorizationFilterContext filterContext, string message = "Invalid Token")
        {
            filterContext.HttpContext.Response.Headers.Add("AuthStatus", "NotAuthorized");

            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Not Authorized";
            filterContext.Result = new JsonResult("NotAuthorized")
            {
                Value = new
                {
                    Result = Result.InvalidSession,
                    Message = message
                },
            };
        }
    }
}
