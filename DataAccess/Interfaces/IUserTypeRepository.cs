using Entities.BE;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Interfaces
{
    public interface IUserTypeRepository
    {
        List<UserTypeBE> GetAll(UserTypeBE userTypeBE);
        int Add(UserTypeBE userTypeBE);
        bool Update(UserTypeBE userTypeBE);
        bool Delete(UserTypeBE userTypeBE);
    }
}
