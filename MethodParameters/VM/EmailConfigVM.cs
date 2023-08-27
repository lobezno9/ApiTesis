using System;
using System.Collections.Generic;
using System.Text;

namespace MethodParameters.VM
{
    public class EmailConfigVM
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public bool IsEnabledSsl { get; set; }
    }
}
