using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.BE
{
    public class OptionBE
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public int ParentId { get; set; }
        public int OrderMenu { get; set; }
        public string Token { get; set; }
        public string Icon { get; set; }
        public string Title { get; set; }
    }
}
