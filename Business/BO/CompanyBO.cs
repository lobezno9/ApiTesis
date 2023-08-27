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
    public class CompanyBO : ICompanyBO
    {
        ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        public CompanyBO(IMapper mapper, ProjectContext context)
        {
            _companyRepository = new CompanyRepository(context);
            _mapper = mapper;
        }


        public GetAllCompanyOut GetAll(GetAllCompanyIn getAllCompanyIn)
        {
            GetAllCompanyOut getAllCompanyOut = new GetAllCompanyOut();
            getAllCompanyIn = getAllCompanyIn ?? new GetAllCompanyIn();
            List<CompanyBE> listCompanyBE = _companyRepository.GetAll(_mapper.Map<CompanyBE>(getAllCompanyIn.Company ?? new CompanyVM()));
            List<CompanyVM> listCompanyVM = _mapper.Map<List<CompanyVM>>(listCompanyBE);

            getAllCompanyOut.Result = MethodParameters.General.Result.Success;
            getAllCompanyOut.ListCompany = listCompanyVM.OrderByDescending(x => x.Id).ToList();

            return getAllCompanyOut;
        }

        public AddCompanyOut Add(AddCompanyIn addCompanyIn)
        {
            AddCompanyOut addCompanyOut = new AddCompanyOut();
            addCompanyIn.Company.CreationDate = DateTime.Now;
            addCompanyIn.Company.ModificationDate = addCompanyIn.Company.CreationDate;
            int newId = _companyRepository.Add(_mapper.Map<CompanyBE>(addCompanyIn.Company));
            addCompanyOut.Result = newId > 0 ? MethodParameters.General.Result.Success : MethodParameters.General.Result.Error;
            addCompanyOut.Id = newId;

            return addCompanyOut;
        }

        public UpdateCompanyOut Update(UpdateCompanyIn updateCompanyIn)
        {
            UpdateCompanyOut updateCompanyOut = new UpdateCompanyOut();
            updateCompanyIn.Company.ModificationDate = DateTime.Now;
            bool result = _companyRepository.Update(_mapper.Map<CompanyBE>(updateCompanyIn.Company));
            updateCompanyOut.Result = result ? MethodParameters.General.Result.Success : MethodParameters.General.Result.Error;

            return updateCompanyOut;
        }
    }
}
