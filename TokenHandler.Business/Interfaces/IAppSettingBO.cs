using System;
using System.Collections.Generic;
using System.Text;

namespace TokenHandler.Business.Interfaces
{
    public interface IAppSettingBO
    {
        Dictionary<string, string> GetAllByGroup(string group);
    }
}
