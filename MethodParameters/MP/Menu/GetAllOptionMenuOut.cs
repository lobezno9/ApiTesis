using System;
using System.Collections.Generic;
using System.Text;
using MethodParameters.General;
using MethodParameters.VM;

namespace MethodParameters.MP
{
    public class GetAllOptionMenuOut : BaseOut
    {
        public List<MenuVM> ListMenu { get; set; }
        public string FullName { get; set; }
        public bool IsSuperAdmin { get; set; }
        public byte[] LogoCompany { get; set; }
    }
}
