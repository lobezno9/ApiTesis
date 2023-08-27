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
    public class PermissionBO : IPermissionBO
    {
        IPermissionRepository _permissionRepository;
        private readonly IMapper _mapper;
        public PermissionBO(IMapper mapper, ProjectContext context)
        {
            _permissionRepository = new PermissionRepository(context);
            _mapper = mapper;
        }
        public GetPermissionOut GetAll(GetPermissionIn getPermissionIn)
        {
            GetPermissionOut getAllPermissionOut = new GetPermissionOut();
            List<PermissionBE> listPermissionBE = _permissionRepository.GetAll(_mapper.Map<PermissionBE>(getPermissionIn.Permission ?? new PermissionVM()),null);
            List<PermissionVM> listPermissionVM = _mapper.Map<List<PermissionVM>>(listPermissionBE);

            getAllPermissionOut.Result = MethodParameters.General.Result.Success;

            getAllPermissionOut.ListPermission = listPermissionVM;
            return getAllPermissionOut;
        }

        public AddPermissionOut Add(AddPermissionIn addPermissionIn)
        {
            AddPermissionOut addPermissionOut = new AddPermissionOut();
            addPermissionIn.Permission.Description = addPermissionIn.Permission.Description.ToUpper();
            int newId = _permissionRepository.Add(_mapper.Map<PermissionBE>(addPermissionIn.Permission));
            addPermissionOut.Result = newId > 0 ? MethodParameters.General.Result.Success : MethodParameters.General.Result.Error;
            addPermissionOut.Id = newId;

            return addPermissionOut;
        }

        public UpdatePermissionOut Update(UpdatePermissionIn updatePermissionIn)
        {
            UpdatePermissionOut updatePermissionOut = new UpdatePermissionOut();
            updatePermissionIn.Permission.Description = updatePermissionIn.Permission.Description.ToUpper();
            bool result = _permissionRepository.Update(_mapper.Map<PermissionBE>(updatePermissionIn.Permission));
            updatePermissionOut.Result = result ? MethodParameters.General.Result.Success : MethodParameters.General.Result.Error;

            return updatePermissionOut;
        }

        public UpdatePermissionOut Delete(UpdatePermissionIn updatePermissionIn)
        {
            UpdatePermissionOut updatePermissionOut = new UpdatePermissionOut();
            bool result = _permissionRepository.Delete(_mapper.Map<PermissionBE>(updatePermissionIn.Permission));
            updatePermissionOut.Result = result ? MethodParameters.General.Result.Success : MethodParameters.General.Result.Error;

            return updatePermissionOut;
        }
    }
}
