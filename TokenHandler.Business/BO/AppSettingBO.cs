using System;
using System.Collections.Generic;
using System.Text;
using TokenHandler.Business.Interfaces;
using TokenHandler.Data.Context;
using TokenHandler.DataAccess.Interfaces;
using TokenHandler.DataAccess.Repositories;
using TokenHandler.Entities.BE;

namespace TokenHandler.Business.BO
{
    public class AppSettingBO : IAppSettingBO
    {
        IAppSettingRepository _iappSettingRepository;

        public AppSettingBO(TokenHandlerContext context)
        {
            _iappSettingRepository = new AppSettingRepository(context);
        }

        public Dictionary<string, string> GetAllByGroup(string group)
        {
            List<AppSettingBE> listAppteSettingBE = _iappSettingRepository.GetAll(new AppSettingBE());
            if (listAppteSettingBE != null)
            {
                Dictionary<string, string> returnList = new Dictionary<string, string>();
                listAppteSettingBE.ForEach(x => returnList.Add(x.Key, x.Value));

                return returnList;
            }
            return null;
        }
    }
}
