using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Entities.BE;
using MethodParameters.MP;
using MethodParameters.VM;

namespace ApiRest.Extensions
{
    public class AutoMapping : Profile
    {

        public AutoMapping()
        {
            CreateMap<UserBE, LoginIn>().ReverseMap();
            CreateMap<LoginOut, UserBE>().ReverseMap();

            #region BE
            CreateMap<UserBE, UserVM>().ReverseMap();
            CreateMap<CompanyBE, CompanyVM>().ReverseMap();
            CreateMap<OptionBE, OptionVM>().ReverseMap();
            CreateMap<ProfileBE, ProfileVM>().ReverseMap();
            CreateMap<AppSettingBE, AppSettingVM>().ReverseMap();
            CreateMap<UserBE, ValidateRecoverPasswordVM>().ReverseMap();
            CreateMap<PermissionBE, PermissionVM>().ReverseMap();
            CreateMap<PermissionsByOptionBE, PermissionsByOptionVM>().ReverseMap();
            CreateMap<UserTypeBE, UserTypeVM>().ReverseMap();

            #endregion
        }
    }
}
