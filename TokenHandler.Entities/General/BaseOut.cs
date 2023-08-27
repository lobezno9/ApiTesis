using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;


namespace TokenHandler.Entities.General
{
    public class BaseOut
    {
        public BaseOut()
        {
            this.Result = Result.Error;
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public Result Result { get; set; }
        public string Message { get; set; }
    }
}
