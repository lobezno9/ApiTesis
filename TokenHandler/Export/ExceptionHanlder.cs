using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;
using TokenHandler.Business.BO;
using TokenHandler.Business.Interfaces;
using TokenHandler.Data.Context;
using TokenHandler.Entities.General;
using TokenHandler.Entities.VM;

namespace TokenHandler.Export
{
    public static class ExceptionHanlder
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        IConfiguration configuration = context.RequestServices.GetService(typeof(IConfiguration)) as IConfiguration;

                        if (!string.IsNullOrEmpty(configuration.GetConnectionString("ProjectContext")))
                        {
                            TokenHandlerContext contextDataBase = new TokenHandlerContext(configuration.GetConnectionString("ProjectContext"));
                            IExceptionLogBO exceptionLogBO = new ExceptionLogBO(contextDataBase);

                            MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();

                            exceptionLogBO.Add(new Entities.BE.ExceptionLogBE()
                            {
                                Date = DateTime.Now,
                                Exception = contextFeature.Error.Message.ToString(),
                                StackTrace = contextFeature.Error.StackTrace.ToString(),
                                Namespace = contextFeature.Error.Source.ToString(),
                                Method = contextFeature.Error.TargetSite.ToString(),
                                CustomData = "TokenHandler.Export Library"
                            });
                        }

                        context.Response.StatusCode = (int)HttpStatusCode.OK;

                        await context.Response.WriteAsync(new BaseOut()
                        {
                            Result = Result.GenericError,
                            Message = "Internal Server Error."
                        }.ToString());
                    }
                });
            });
        }
    }
}
