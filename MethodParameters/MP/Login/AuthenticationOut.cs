using System;
using System.Collections.Generic;
using System.Text;
using MethodParameters.General;

namespace MethodParameters.MP
{
    public class AuthenticationOut : BaseOut
    {
        public string Token { get; set; }
        public DateTime Expire { get; set; }
        public bool IsAuthetnicated { get; set; }
        public bool IsActive { get; set; }
    }
}