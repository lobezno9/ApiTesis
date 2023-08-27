using MethodParameters.General;
using MethodParameters.VM;
using System;
using System.Collections.Generic;
using System.Text;

namespace MethodParameters.MP
{
    public class GetUserTypeOut : BaseOut
    {
        public List<UserTypeVM> ListUserType { get; set; }
    }
}
