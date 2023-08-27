using MethodParameters.MP;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interfaces
{
    public interface IUserTypeBO
    {
        GetUserTypeOut GetAll(GetUserTypeIn getUserTypeIn);
        AddUserTypeOut Add(AddUserTypeIn addUserTypeIn);
        UpdateUserTypeOut Update(UpdateUserTypeIn updateUserTypeIn);
        UpdateUserTypeOut Delete(UpdateUserTypeIn updateUserTypeIn);
    }
}
