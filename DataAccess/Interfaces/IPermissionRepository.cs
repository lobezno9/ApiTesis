using Entities.BE;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Interfaces
{
    public interface IPermissionRepository
    {
        List<PermissionBE> GetAll(PermissionBE permissionBE , int? id);
        int Add(PermissionBE permissionBE);
        bool Update(PermissionBE permissionBE);
        bool Delete(PermissionBE permissionBE);
    }
}
