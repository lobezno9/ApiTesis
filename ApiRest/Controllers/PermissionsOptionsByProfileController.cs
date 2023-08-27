using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MethodParameters.MP;
using TokenHandler.Export;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Business.Interfaces;

namespace ApiRest.Controllers
{
    [ProjectTesisAuthorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsOptionsByProfileController : ControllerBase
    {
        public readonly IPermissionsOptionsByProfileBO _permissionsOptionsByProfileBO;
        public PermissionsOptionsByProfileController(IPermissionsOptionsByProfileBO ipermissionsOptionsByProfileBO)
        {
            _permissionsOptionsByProfileBO = ipermissionsOptionsByProfileBO;
        }

        [HttpPost("GetAll")]
        public IActionResult GetAll(GetPermissionsOptionsByProfileIn getPermissionsOptionsByProfileIn)
        {
            return Ok(_permissionsOptionsByProfileBO.GetAll(getPermissionsOptionsByProfileIn));
        }
    }
}
