using System;
using System.Collections.Generic;
using System.Text;
using TokenHandler.Business.Interfaces;
using TokenHandler.Data.Context;
using TokenHandler.Entities.BE;
using TokenHandler.Entities.VM;

namespace TokenHandler.Business.BO
{
    public class TokenValidateBO : ITokenValidateBO
    {
        TokenHandlerContext _context;
        public TokenValidateBO(TokenHandlerContext context)
        {
            _context = context;
        }

        public TokenValidateVM IsActiveUser(int companyId, int userId)
        {
            TokenValidateVM result = new TokenValidateVM();
            ICompanyBO companyBO = new CompanyBO(_context);
            CompanyBE companyBE = companyBO.GetById(companyId);
            result.IsActiveCompany = !(companyBE == null || (companyBE.IsActive.HasValue && !companyBE.IsActive.Value));
            if (!result.IsActiveCompany)
                return result;

            IUserBO userBO = new UserBO(_context);
            UserBE userBE = userBO.GetById(userId);
            result.IsActiveUser = !(userBE == null || (userBE.IsActive.HasValue && !userBE.IsActive.Value));

            return result;
        }
    }
}
