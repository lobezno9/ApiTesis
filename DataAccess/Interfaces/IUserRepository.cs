using System;
using System.Collections.Generic;
using System.Text;
using Entities.BE;

namespace DataAccess.Interfaces
{
    public interface IUserRepository
    {
        List<UserBE> GetAll(UserBE farmBE);
        int Add(UserBE userBE);
        bool Update(UserBE userBE);
        bool Delete(UserBE userBE);
        /// <summary>
        /// Validates if the user name and the email match
        /// </summary>
        /// <param name="userBE">Get the username and email for execute the spr_Users_ValidateRecoverPasswordCommand</param>
        /// <returns>boolean value with true if the username and email match</returns>
        bool ValidateRecoverPassword(UserBE userBE);
        List<UserBE> GetAll();
    }
}
