using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Business.Interfaces;
using Data.Context;
using DataAccess.Interfaces;
using DataAccess.Repository;
using Entities.BE;
using MethodParameters.VM;

namespace Business.BO
{
    public class AppSettingBO : IAppSettingBO
    {
        IAppSettingRepository _iappSettingRepository;
        private readonly IMapper _mapper;
        public AppSettingBO(IMapper mapper, ProjectContext context)
        {
            _iappSettingRepository = new AppSettingRepository(context);
            _mapper = mapper;
        }
        public List<AppSettingVM> GetAll(AppSettingVM appSettingVM)
        {
            List<AppSettingBE> listAppSettingBE = _iappSettingRepository.GetAll(_mapper.Map<AppSettingBE>(appSettingVM));
            List<AppSettingVM> returnList = _mapper.Map<List<AppSettingVM>>(listAppSettingBE);
            return returnList;
        }
    }
}
