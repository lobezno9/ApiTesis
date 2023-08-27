using System;
using System.Collections.Generic;
using System.Text;
using TokenHandler.Entities.BE;

namespace TokenHandler.DataAccess.Interface
{
    public interface ICompanyRepository
    {
        CompanyBE GetById(int companyId);
    }
}
