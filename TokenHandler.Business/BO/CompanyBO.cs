using System;
using System.Collections.Generic;
using System.Text;
using TokenHandler.Business.Interfaces;
using TokenHandler.Data.Context;
using TokenHandler.DataAccess.Interface;
using TokenHandler.DataAccess.Repositories;
using TokenHandler.Entities.BE;

namespace TokenHandler.Business.BO
{
    public class CompanyBO : ICompanyBO
    {
        ICompanyRepository _iCompanyRepository;

        public CompanyBO(TokenHandlerContext context)
        {
            _iCompanyRepository = new CompanyRepository(context);
        }

        public CompanyBE GetById(int companyId)
        {
            CompanyBE companyBE = _iCompanyRepository.GetById(companyId);
            return companyBE;
        }
    }
}
