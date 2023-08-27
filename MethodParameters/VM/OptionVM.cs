using System;
using System.Collections.Generic;
using System.Text;

namespace MethodParameters.VM
{
    public class OptionVM
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public int ParentId { get; set; }
        public int OrderMenu { get; set; }
        public string Token { get; set; }
        public string Icon { get; set; }
        public bool IsParent { get; set; }
        public string Title { get; set; }
        public bool HasChild { get; set; }
        public bool IsToRemove { get; set; }
        public bool IsChecked { get; set; }
        public List<OptionVM> ListOption { get; set; }
        public List<PermissionVM> ListPermission { get; set; }
    }
}
