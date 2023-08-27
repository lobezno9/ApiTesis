using System;
using System.Collections.Generic;
using System.Text;
using TokenHandler.Entities.BE;

namespace TokenHandler.Business.Interfaces
{
    public interface ICompanyBO
    {
        CompanyBE GetById(int companyId);
    }
}
