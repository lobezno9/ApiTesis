using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiRest.Services;
using ApiRest.Services.Interfaces;
using Business.BO;
using Business.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ApiRest.Extensions
{
    public static class AddEntityServices
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUserBO, UserBO>();
            services.AddTransient<ICompanyBO, CompanyBO>();
            services.AddTransient<IOptionBO, OptionBO>();
            services.AddTransient<IProfileBO, ProfileBO>();
            services.AddTransient<IAppSettingBO, AppSettingBO>();
            services.AddTransient<IPermissionBO, PermissionBO>();
            services.AddTransient<IPermissionsByOptionBO, PermissionsByOptionBO>();
            services.AddTransient<IUserTypeBO, UserTypeBO>();

        }
    }
}
