using System;
using System.Collections.Generic;
using System.Text;
using MethodParameters.General;
using MethodParameters.VM;

namespace MethodParameters.MP
{
    public class GetAllOptionsByProfileOut : BaseOut
    {
        public List<OptionsByProfileVM> ListOptionsByProfileVM { get; set; }
    }
}
