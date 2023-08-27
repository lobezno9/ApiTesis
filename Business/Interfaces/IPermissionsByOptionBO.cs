using MethodParameters.MP.Menu;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interfaces
{
    public interface IPermissionsByOptionBO
    {
        GetPermissionsByOptionOut GetAll(GetPermissionsByOptionIn getPermissionsByOptionIn);
        AddPermissionsByOptionOut Add(AddPermissionsByOptionIn addPermissionsByOptionIn);
    }
}
