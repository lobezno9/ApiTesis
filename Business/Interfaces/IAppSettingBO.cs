using System;
using System.Collections.Generic;
using System.Text;
using MethodParameters.VM;

namespace Business.Interfaces
{
    public interface IAppSettingBO
    {
        List<AppSettingVM> GetAll(AppSettingVM appSettingVM);
    }
}
