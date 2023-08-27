using System;
using System.Collections.Generic;
using System.Text;
using Entities.BE;

namespace DataAccess.Interfaces
{
    public interface IAppSettingRepository
    {
        List<AppSettingBE> GetAll(AppSettingBE appSettingBE);
    }
}
