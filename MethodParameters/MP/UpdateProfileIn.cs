using System;
using System.Collections.Generic;
using System.Text;
using MethodParameters.General;
using MethodParameters.VM;

namespace MethodParameters.MP
{
    public class UpdateProfileIn : BaseIn
    {
        public ProfileVM Profile { get; set; }
        public List<OptionVM> ListOption { get; set; }
    }
}
