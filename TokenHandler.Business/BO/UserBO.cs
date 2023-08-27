using System;
using System.Collections.Generic;
using System.Text;
using TokenHandler.Business.Interfaces;
using TokenHandler.Data.Context;
using TokenHandler.DataAccess.Interfaces;
using TokenHandler.DataAccess.Repositories;
using TokenHandler.Entities.BE;

namespace TokenHandler.Business.BO
{
    public class UserBO : IUserBO
    {
        IUserRepository _iUserRepository;

        public UserBO(TokenHandlerContext context)
        {
            _iUserRepository = new UserRepository(context);
        }

        public UserBE GetById(int userId)
        {
            UserBE userBE = _iUserRepository.GetById(userId);
            return userBE;
        }
    }
}
