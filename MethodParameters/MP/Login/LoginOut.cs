using System;
using System.Collections.Generic;
using System.Text;
using MethodParameters.General;

namespace MethodParameters.MP
{
    public class LoginOut : BaseOut
    {
        public int Id { get; set; }
        public bool IsLoginOk { get; set; }
        public int CompanyId { get; set; }
        public int ProfileId { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public bool IsSuperAdmin { get; set; }
        public string Identification { get; set; }
        public int GourpId { get; set; }
        public int UserTypeId { get; set; }
        public bool IsActive { get; set; }
    }
}
