using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.BE
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
        public string Email { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public string Identification { get; set; }
        public int UserTypeId { get; set; }
        public virtual CompanyBE Company { get; set; }
        public virtual ProfileBE Profile { get; set; }
        public virtual UserTypeBE UserType { get; set; }
    }
}
