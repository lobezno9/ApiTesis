using MethodParameters.General;
using MethodParameters.VM;
using System;
using System.Collections.Generic;
using System.Text;

namespace MethodParameters.MP.Menu
{
    public class GetPermissionsByOptionOut : BaseOut
    {
        public List<PermissionsByOptionVM> ListPermissionsByOption { get; set; }
    }
}
