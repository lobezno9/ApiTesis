using System;
using System.Collections.Generic;
using System.Text;
using TokenHandler.Entities.BE;

namespace TokenHandler.Business.Interfaces
{
    public interface IExceptionLogBO 
    {
        void Add(ExceptionLogBE exceptionLogBE);
    }
}
