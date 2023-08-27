using System;
using System.Collections.Generic;
using System.Text;
using TokenHandler.Entities.VM;

namespace TokenHandler.Business.Interfaces
{
    public interface ITokenValidateBO
    {
        TokenValidateVM IsActiveUser(int companyId, int userId);
    }
}
