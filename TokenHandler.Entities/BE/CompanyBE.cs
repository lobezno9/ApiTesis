using System;
using System.Collections.Generic;
using System.Text;

namespace TokenHandler.Entities.BE
{
    public class CompanyBE
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DatabaseName { get; set; }
        public Boolean? IsActive { get; set; }
    }
}
