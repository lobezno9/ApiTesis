using System;
using System.Collections.Generic;
using System.Text;
using MethodParameters.MP;

namespace Business.Interfaces
{
    public interface ICompanyBO
    {
        GetAllCompanyOut GetAll(GetAllCompanyIn getAllCompanyOut);
        AddCompanyOut Add(AddCompanyIn addCompanyIn);
        UpdateCompanyOut Update(UpdateCompanyIn updateCompanyIn);
    }
}
