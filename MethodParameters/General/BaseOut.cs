using System;
using System.Collections.Generic;
using System.Text;

namespace MethodParameters.General
{
    public class BaseOut
    {
        public BaseOut()
        {
            this.Result = Result.Error;
        }

        public Result Result { get; set; }
        public string Message { get; set; }
    }
}
