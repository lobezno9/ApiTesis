using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TokenHandler.Business.BO;
using TokenHandler.Business.Interfaces;
using TokenHandler.Data.Context;


namespace TokenHandler.Export
{
    public static class JwtHanlder
    {

        public static void JwtConfigutration(this IServiceCollection services, string connectionString)
        {
            IAppSettingBO appSettingBO = new AppSettingBO(new TokenHandlerContext(connectionString));
            Dictionary<string, string> listAppSettingVM = appSettingBO.GetAllByGroup("JWT");
            if (listAppSettingVM != null)
            {
                services.AddAuthorization(option =>
                {
                    option.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme).
                    RequireAuthenticatedUser().
                    Build();
                });

                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
                {
                    option.Audience = listAppSettingVM["Audience"];
                    option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = listAppSettingVM["Issuer"],
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(listAppSettingVM["SigningKey"]))
                    };
                });
            }
        }
    }
}
