using System;
using System.Collections.Generic;
using System.Text;
using MethodParameters.MP;

namespace Business.Interfaces
{
    public interface IOptionBO
    {
        GetAllOptionOut GetAll(GetAllOptionIn getAllOptionOut);
        GetAllOptionMenuOut GetAllMenu(GetAllOptionIn getAllOptionIn);
        AddOptionOut Add(AddOptionIn addOptionIn);
        UpdateOptionOut Update(UpdateOptionIn updateOptionIn);
        AddOptionMenuOut AddMenu(AddOptionMenuIn addOptionMenuIn);
    }
}
