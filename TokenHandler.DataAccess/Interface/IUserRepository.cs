using System;
using System.Collections.Generic;
using System.Text;
using TokenHandler.Entities.BE;

namespace TokenHandler.DataAccess.Interfaces
{
    public interface IUserRepository
    {
        UserBE GetById(int userId);
    }
}
