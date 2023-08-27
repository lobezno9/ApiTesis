using System;
using System.Collections.Generic;
using System.Text;

namespace MethodParameters.VM
{
    public class UserVM
    {
        public int? Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CompanyId { get; set; }
        public int ProfileId { get; set; }
        public string Email { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string Identification { get; set; }
        public int UserTypeId { get; set; }
        public ProfileVM Profile { get; set; }
        public CompanyVM Company { get; set; }
        public UserTypeVM UserType { get; set; }

    }
}
