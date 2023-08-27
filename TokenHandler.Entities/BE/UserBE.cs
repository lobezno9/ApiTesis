using System;
using System.Collections.Generic;
using System.Text;

namespace TokenHandler.Entities.BE
{
    public class UserBE
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CompanyId { get; set; }
        public int ProfileId { get; set; }
        public bool? IsActive { get; set; }
    }
}
