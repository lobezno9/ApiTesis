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
using MethodParameters.MP;
using MethodParameters.VM;

namespace Business.BO
{
    public class OptionBO : IOptionBO
    {
        IOptionRepository _optionRepository;
        IPermissionRepository _permissionsRepository;
        IPermissionsByOptionRepository _permissionsByOptionRepository;
        IPermissionsOptionsByProfileRepository _permissionsOptionsByProfileRepository;

        private readonly IMapper _mapper;
        public OptionBO(IMapper mapper, ProjectContext context)
        {
            _optionRepository = new OptionRepository(context);
            _permissionsRepository = new PermissionRepository(context);
            _permissionsByOptionRepository = new PermissionsByOptionRepository(context);
            _permissionsOptionsByProfileRepository = new PermissionsOptionsByProfileRepository(context);
            _mapper = mapper;
        }

        public GetAllOptionMenuOut GetAllMenu(GetAllOptionIn getAllOptionIn)
        {
            GetAllOptionMenuOut getAllOptionMenuOut = new GetAllOptionMenuOut();

            List<OptionBE> listOptionBE;
            //Consulta la lista de opciones por perfil si IsToProfileManager es true o todas si es false
            if (!getAllOptionIn.IsToProfileManager)
                listOptionBE = _optionRepository.GetAll(_mapper.Map<OptionBE>(getAllOptionIn.Option ?? new OptionVM()), getAllOptionIn.ProfileId);
            else
                listOptionBE = _optionRepository.GetAll(_mapper.Map<OptionBE>(getAllOptionIn.Option ?? new OptionVM()), null);

            //Convert de List de OptionBE a List de OptionVM
            List<OptionVM> listOptionVM = _mapper.Map<List<OptionVM>>(listOptionBE);
            //Recorre la lista de opciones y en cada posicion de la lista , le agrega la lista de permisos correspondiente 
            listOptionVM.ForEach(x => x.ListPermission = _mapper.Map<List<PermissionVM>>(_permissionsRepository.GetAll(new PermissionBE(), x.Id)));

            //Consulta las opciones del perfil y los permisos que tenga 
            if (getAllOptionIn.ProfileId > 0 && getAllOptionIn.IsToProfileManager)
            {
                //Trae una lista de las opciones que tenga el perfil
                List<OptionBE> listOptionPrfole = _optionRepository.GetAll(_mapper.Map<OptionBE>(getAllOptionIn.Option ?? new OptionVM()), getAllOptionIn.ProfileId);
                var tasada = listOptionVM.Where(x => listOptionPrfole.Select(lp => lp.Id).ToList().Contains(x.Id)).ToList();
                //De listOptionVM y listOptionPrfole se cruzan y las opciones que considan se marcan IsChecked= true
                listOptionVM.Where(x => listOptionPrfole.Select(lp => lp.Id).ToList().Contains(x.Id)).ToList().ForEach(x => x.IsChecked = true);
            }
            //Recoremos listOptionVM 
            listOptionVM.ForEach(x => x.ListPermission.ForEach(p =>
            {
                //Consulta los permisos de cada perfil en cada una de las opciones opciones 
                List<PermissionVM> listPermissionPrOfile = ConvertListPermissionsOptionsByProfileBEToListPermissionVM(_permissionsOptionsByProfileRepository.GetAll(
                    new PermissionsOptionsByProfileBE() { IdProfile = getAllOptionIn.ProfileId, IdOption = x.Id }));
                //De listOptionVM.ListPermission y listPermissionPrOfile se cruzan y los permisos que considan se marcan IsChecked= true
                x.ListPermission.Where(l => listPermissionPrOfile.Select(lp => lp.Id).ToList().Contains(l.Id)).ToList().ForEach(f => f.IsChecked = true);
            }));

            List<OptionVM> listOptionParent = listOptionVM.Where(x => listOptionVM.Select(l => l.ParentId).ToList().Contains(x.Id) || (!string.IsNullOrEmpty(x.Title) && !listOptionVM.Select(l => l.ParentId).ToList().Contains(x.Id))).ToList();
            listOptionParent.ForEach(x => { x.IsParent = true; x.HasChild = listOptionVM.Any(c => c.ParentId == x.Id); x.ListOption = listOptionVM.Where(c => c.ParentId == x.Id).ToList(); });
            listOptionParent.RemoveAll(x => x.ParentId > 0);
            getAllOptionMenuOut.Result = MethodParameters.General.Result.Success;

            var listGroupByTitle = listOptionParent.GroupBy(x => x.Title).ToList();

            List<MenuVM> lisResult = new List<MenuVM>();
            listGroupByTitle.ForEach(x => lisResult.Add(new MenuVM()
            {
                Title = x.Key,
                ListOption = x.ToList()
            }));

            getAllOptionMenuOut.ListMenu = lisResult;
            return getAllOptionMenuOut;
        }

        public GetAllOptionOut GetAll(GetAllOptionIn getAllOptionIn)
        {
            GetAllOptionOut getAllOptionOut = new GetAllOptionOut();
            List<OptionBE> listOptionBE = _optionRepository.GetAll(_mapper.Map<OptionBE>(getAllOptionIn.Option ?? new OptionVM()), getAllOptionIn.ProfileId);
            List<OptionVM> listOptionVM = _mapper.Map<List<OptionVM>>(listOptionBE);

            getAllOptionOut.Result = MethodParameters.General.Result.Success;

            getAllOptionOut.ListOption = listOptionVM;
            return getAllOptionOut;
        }

        public AddOptionOut Add(AddOptionIn addOptionIn)
        {
            AddOptionOut addOptionOut = new AddOptionOut();
            int newId = _optionRepository.Add(_mapper.Map<OptionBE>(addOptionIn.Option));
            addOptionOut.Result = newId > 0 ? MethodParameters.General.Result.Success : MethodParameters.General.Result.Error;
            addOptionOut.Id = newId;

            return addOptionOut;
        }

        public UpdateOptionOut Update(UpdateOptionIn updateOptionIn)
        {
            UpdateOptionOut updateOptionOut = new UpdateOptionOut();
            bool result = _optionRepository.Update(_mapper.Map<OptionBE>(updateOptionIn.Option));
            updateOptionOut.Result = result ? MethodParameters.General.Result.Success : MethodParameters.General.Result.Error;

            return updateOptionOut;
        }

        public AddOptionMenuOut AddMenu(AddOptionMenuIn addOptionMenuIn)
        {
            foreach (var item in addOptionMenuIn.ListMenu)
            {
                if (item != null && item.ListOption != null && item.ListOption.Any())
                {
                    item.ListOption.ForEach(x => RecursiveOptionMenu(x, item.Title, 0));
                }
            }

            return new AddOptionMenuOut()
            {
                Result = MethodParameters.General.Result.Success
            };
        }

        private void RecursiveOptionMenu(OptionVM optionVM, string title, int parentId)
        {
            optionVM.Title = title;

            optionVM.ParentId = parentId;
            if (!optionVM.IsToRemove)
            {
                if (optionVM.Id > 0)
                {
                    _optionRepository.Update(_mapper.Map<OptionBE>(optionVM));

                    if (optionVM.ListPermission != null)
                    {
                        _permissionsByOptionRepository.Delete(new PermissionsByOptionBE() { IdOption = optionVM.Id });
                        if (optionVM.ListPermission != null && optionVM.ListPermission.Count > 0)
                        {
                            //foreach (var item in optionVM.ListPermission)
                            //{
                            //    _permissionsByOptionRepository.Add(new PermissionsByOptionBE()
                            //    {
                            //        IdOption = optionVM.Id,
                            //        IdPermission = item.Id
                            //    });
                            //}
                            optionVM.ListPermission.ForEach(x => _permissionsByOptionRepository.Add(new PermissionsByOptionBE()
                            {
                                IdOption = optionVM.Id,
                                IdPermission = x.Id
                            }));

                        }
                    }
                }
                else if (!string.IsNullOrEmpty(optionVM.Description))
                {
                    optionVM.Id = _optionRepository.Add(_mapper.Map<OptionBE>(optionVM));
                    if (optionVM.ListPermission != null && optionVM.ListPermission.Count > 0)
                    {
                        //foreach (var item in optionVM.ListPermission)
                        //{
                        //    PermissionsByOptionBE permissionsByOption = new PermissionsByOptionBE();
                        //    permissionsByOption.IdOption = optionVM.Id;
                        //    permissionsByOption.IdPermission = item.Id;
                        //    _permissionsByOptionRepository.Add(permissionsByOption);
                        //}
                        //PermissionsByOptionBE permissionsByOption = new PermissionsByOptionBE() { IdOption = optionVM.Id };
                        optionVM.ListPermission.ForEach(x => {
                            //permissionsByOption.IdPermission = x.Id;
                            _permissionsByOptionRepository.Add(new PermissionsByOptionBE() { IdOption = optionVM.Id, IdPermission = x.Id });
                        });
                    }
                }

                if (optionVM.ListOption != null && optionVM.ListOption.Any())
                {
                    optionVM.ListOption.ForEach(x => RecursiveOptionMenu(x, title, optionVM.Id));
                }
            }
            else if (optionVM.Id > 0)
            {
                _optionRepository.Remove(optionVM.Id);
                if (optionVM.ListOption != null && optionVM.ListOption.Any())
                {
                    optionVM.ListOption.ForEach(x => { x.IsToRemove = true; RecursiveOptionMenu(x, title, optionVM.Id); });
                }
            }
        }


        private List<PermissionVM> ConvertListPermissionsOptionsByProfileBEToListPermissionVM(List<PermissionsOptionsByProfileBE> list)
        {
            List<PermissionVM> listReturn = new List<PermissionVM>();
            if (list != null && list.Any())
            {
                list.ForEach(x => listReturn.Add(new PermissionVM()
                {
                    Id = x.IdPermission,
                    Description=x.Description
                }
                ));
            }
            return listReturn;
        }
    }

}
