using System;
using System.Collections.Generic;
using System.Text;
using MethodParameters.General;
using MethodParameters.VM;

namespace MethodParameters.MP
{
    public class GetAllOptionIn : BaseIn
    {
        public OptionVM Option { get; set; }
        public int ProfileId { get; set; }
        public bool IsToProfileManager { get; set; }
    }
}
