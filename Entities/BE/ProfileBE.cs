using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.BE
{
    public class ProfileBE
    {
        public int Id { get; set; }
        public string ProfileCode { get; set; }
        public string Description { get; set; }
        public bool IsSuperAdmin { get; set; }
    }
}
