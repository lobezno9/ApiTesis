using MethodParameters.General;
using MethodParameters.VM;
using System;
using System.Collections.Generic;
using System.Text;

namespace MethodParameters.MP.Menu
{
    public class GetPermissionOut : BaseOut
    {
        public List<PermissionVM> ListPermission { get; set; }
    }
}
