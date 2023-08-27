using Entities.BE;
using MethodParameters.MP;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interfaces
{
    public interface IPermissionsOptionsByProfileBO
    {
        GetPermissionsOptionsByProfileOut GetAll(GetPermissionsOptionsByProfileIn getPermissionsOptionsByProfileIn);

        int Add(PermissionsOptionsByProfileBE permissionsOptionsByProfileBE);

        bool Delete(PermissionsOptionsByProfileBE permissionsOptionsByProfileBE);
    }
}
