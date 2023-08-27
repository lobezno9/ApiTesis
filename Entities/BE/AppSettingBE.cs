using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.BE
{
    public class AppSettingBE
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Group { get; set; }
    }
}
