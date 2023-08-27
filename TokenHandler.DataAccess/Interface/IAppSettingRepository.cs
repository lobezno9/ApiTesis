using System;
using System.Collections.Generic;
using System.Text;
using TokenHandler.Entities.BE;

namespace TokenHandler.DataAccess.Interfaces
{
    public interface IAppSettingRepository
    {
        List<AppSettingBE> GetAll(AppSettingBE appSettingBE);
    }
}
