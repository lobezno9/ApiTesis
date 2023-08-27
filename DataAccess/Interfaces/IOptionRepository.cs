using System;
using System.Collections.Generic;
using System.Text;
using Entities.BE;

namespace DataAccess.Interfaces
{
    public interface IOptionRepository
    {
        List<OptionBE> GetAll(OptionBE optionBE, int? profileId);
        int Add(OptionBE OptionBE);
        bool Update(OptionBE OptionBE);
        bool Remove(int id);
    }

}
