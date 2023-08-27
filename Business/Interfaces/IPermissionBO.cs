using Entities.BE;
using MethodParameters.MP.Menu;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interfaces
{
    public interface IPermissionBO
    {
        GetPermissionOut GetAll(GetPermissionIn getPermissionIn);
        AddPermissionOut Add(AddPermissionIn addPermissionIn);
        UpdatePermissionOut Update(UpdatePermissionIn updatePermissionIn);
        UpdatePermissionOut Delete(UpdatePermissionIn updatePermissionIn);
    }
}
