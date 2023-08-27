using Entities.BE;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Interfaces
{
    public interface IPermissionsOptionsByProfileRepository
    {
        List<PermissionsOptionsByProfileBE> GetAll(PermissionsOptionsByProfileBE permissionsOptionsByProfileBE);
        int Add(PermissionsOptionsByProfileBE permissionsOptionsByProfileBE);
        bool Delete(PermissionsOptionsByProfileBE permissionsOptionsByProfileBE);
    }
}
