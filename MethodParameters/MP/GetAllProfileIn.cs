using System;
using System.Collections.Generic;
using System.Text;
using MethodParameters.General;
using MethodParameters.VM;

namespace MethodParameters.MP
{
    public class GetAllProfileIn : BaseIn
    {
        public ProfileVM Profile { get; set; }
        public int ProfileId { get; set; }
    }
}
