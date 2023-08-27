using MethodParameters.General;
using MethodParameters.VM;
using System;
using System.Collections.Generic;
using System.Text;

namespace MethodParameters.MP
{
    public class GetPermissionsOptionsByProfileOut : BaseOut
    {
        public List<PermissionsOptionsByProfileVM> listPermissionsOptionsByProfile { get; set; }
    }
}
