using Entities.BE;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Interfaces
{
    public interface IPermissionsByOptionRepository
    {
        List<PermissionsByOptionBE> GetAll(PermissionsByOptionBE permissionsByOptionBE);
        int Add(PermissionsByOptionBE permissionsByOptionBE);
        bool Delete(PermissionsByOptionBE permissionsByOptionBE);
    }
}
