using System;
using System.Collections.Generic;
using System.Text;
using MethodParameters.General;
using MethodParameters.VM;

namespace MethodParameters.MP
{
    public class GetAllUserOut : BaseOut
    {
        public List<UserVM> ListUser { get; set; }
    }
}
