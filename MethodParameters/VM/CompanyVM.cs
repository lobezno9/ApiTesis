using System;
using System.Collections.Generic;
using System.Text;

namespace MethodParameters.VM
{
    public class CompanyVM
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string DatabaseName { get; set; }
        public byte[] ImageLogo { get; set; }
        public Boolean? IsActive { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
}
