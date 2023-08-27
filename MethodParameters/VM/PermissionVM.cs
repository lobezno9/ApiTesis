using System;
using System.Collections.Generic;
using System.Text;

namespace MethodParameters.VM
{
    public class PermissionVM
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsToRemove { get; set; }
        public bool IsChecked { get; set; }
    }
}
