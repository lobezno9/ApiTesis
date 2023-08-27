using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiRest.Services.Interfaces;
using MethodParameters.MP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Business.Interfaces;
using TokenHandler.Export;
using MethodParameters.General;
using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace ApiRest.Controllers
{
    [ProjectTesisAuthorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        public readonly ICompanyBO _iCompanyBO;
        public CompanyController(ICompanyBO iCompanyBO)
        {
            _iCompanyBO = iCompanyBO;
        }


        [HttpPost("GetAll")]
        public IActionResult GetAll(GetAllCompanyIn getAllCompanyOut)
        {
            return Ok(_iCompanyBO.GetAll(getAllCompanyOut));
        }

        [HttpPost("Add")]
        public IActionResult Add(AddCompanyIn addCompanyIn)
        {
            return Ok(_iCompanyBO.Add(addCompanyIn));
        }

        [HttpPost("Update")]
        public IActionResult Update(UpdateCompanyIn updateCompanyIn)
        {
            return Ok(_iCompanyBO.Update(updateCompanyIn));
        }
    }
}