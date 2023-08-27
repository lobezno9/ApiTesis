using System;
using System.Collections.Generic;
using System.Text;

namespace TokenHandler.Entities.BE
{
    public class ExceptionLogBE
    {
        public DateTime Date { get; set; }
        public string Exception { get; set; }
        public string Namespace { get; set; }
        public string StackTrace { get; set; }
        public string Method { get; set; }
        public string CustomData { get; set; }
    }
}
