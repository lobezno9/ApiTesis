using System;
using System.Collections.Generic;
using System.Text;

namespace TokenHandler.Export
{
    public class CustomUserIdentity
    {
        public int CompanyId { get; set; }
        public int ProfileId { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public bool? IsSuperAdmin { get; set; }
    }
}