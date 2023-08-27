using TokenHandler.Business.Interfaces;
using TokenHandler.Data.Context;
using TokenHandler.DataAccess.Interfaces;
using TokenHandler.DataAccess.Repositories;
using TokenHandler.Entities.BE;
using System;
using System.Collections.Generic;
using System.Text;

namespace TokenHandler.Business.BO
{
    public class ExceptionLogBO : IExceptionLogBO
    {
        IExceptionLogRepository _iExceptionLogRepository;

        public ExceptionLogBO(TokenHandlerContext context)
        {
            _iExceptionLogRepository = new ExceptionLogRepository(context);
        }

        public void Add(ExceptionLogBE exceptionLogBE)
        {
            _iExceptionLogRepository.Add(exceptionLogBE);
        }
    }
}
