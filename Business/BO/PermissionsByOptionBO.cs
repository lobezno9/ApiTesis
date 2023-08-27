using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Business.Interfaces;
using Data.Context;
using DataAccess.Interfaces;
using DataAccess.Repository;
using Entities.BE;
using MethodParameters.MP.Menu;
using MethodParameters.VM;

namespace Business.BO
{
    public class PermissionsByOptionBO : IPermissionsByOptionBO
    {
        IPermissionsByOptionRepository _permissionsByOptionRepository;
        private readonly IMapper _mapper;
        public PermissionsByOptionBO(IMapper mapper, ProjectContext context)
        {
            _permissionsByOptionRepository = new PermissionsByOptionRepository(context);
            _mapper = mapper;
        }

        public GetPermissionsByOptionOut GetAll(GetPermissionsByOptionIn getPermissionsByOptionIn)
        {
            GetPermissionsByOptionOut getAllPermissionsByOptionOut = new GetPermissionsByOptionOut();
            List<PermissionsByOptionBE> listPermissionsByOptionBE = _permissionsByOptionRepository.GetAll(_mapper.Map<PermissionsByOptionBE>(getPermissionsByOptionIn.PermissionsByOption ?? new PermissionsByOptionVM()));
            List<PermissionsByOptionVM> listPermissionsByOptionVM = _mapper.Map<List<PermissionsByOptionVM>>(listPermissionsByOptionBE);

            getAllPermissionsByOptionOut.Result = MethodParameters.General.Result.Success;

            getAllPermissionsByOptionOut.ListPermissionsByOption = listPermissionsByOptionVM;
            return getAllPermissionsByOptionOut;
        }

        public AddPermissionsByOptionOut Add(AddPermissionsByOptionIn addPermissionsByOptionIn)
        {
            AddPermissionsByOptionOut addPermissionsByOptionOut = new AddPermissionsByOptionOut();
            int newId = _permissionsByOptionRepository.Add(_mapper.Map<PermissionsByOptionBE>(addPermissionsByOptionIn.PermissionsByOption));
            addPermissionsByOptionOut.Result = newId > 0 ? MethodParameters.General.Result.Success : MethodParameters.General.Result.Error;
            addPermissionsByOptionOut.Id = newId;

            return addPermissionsByOptionOut;
        }
       
    }
}
