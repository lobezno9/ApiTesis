using System;
using System.Collections.Generic;
using System.Text;
using Entities.BE;

namespace DataAccess.Interfaces
{
    public interface ICompanyRepository
    {
        List<CompanyBE> GetAll(CompanyBE companyBE);
        int Add(CompanyBE CompanyBE);
        bool Update(CompanyBE CompanyBE);
    }
}
