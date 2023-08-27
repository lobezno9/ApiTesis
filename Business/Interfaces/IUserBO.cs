using System;
using System.Collections.Generic;
using System.Text;
using MethodParameters.MP;

namespace Business.Interfaces
{
    public interface IUserBO
    {
        LoginOut Login(LoginIn userBE);
        GetAllUserOut GetAll(GetAllUserIn getAllUserOut);
        GetAllUserOut GetAll();
        AddUserOut Add(AddUserIn addUserIn);
        UpdateUserOut Update(UpdateUserIn updateUserIn);
        UpdateUserOut Delete(UpdateUserIn updateUserIn);

        /// <summary>
        /// Update the password of a client by username encrypted
        /// </summary>
        /// <param name="updateUserIn">Entity with the password and username</param>
        /// <returns>Confirmation of the process</returns>
        UpdateUserOut UpdatePassword(UpdateUserIn updateUserIn);

        /// <summary>
        /// Validates if the user name and the email match
        /// </summary>
        /// <param name="userBE">Get the username and email for execute the spr_Users_ValidateRecoverPasswordCommand</param>
        /// <returns>boolean value with true if the username and email match</returns>
        ValidateRecoverPasswordOut ValidateRecoverPassword(ValidateRecoverPasswordIn validateRecoverPasswordIn);
    }

}
