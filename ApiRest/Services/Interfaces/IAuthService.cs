using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MethodParameters.MP;

namespace ApiRest.Services.Interfaces
{
    public interface IAuthService
    {
        LoginOut ValidateLogin(LoginIn loginIn);
        AuthenticationOut GenerateToken(LoginOut loginOut);
    }
}
