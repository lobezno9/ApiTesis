using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using MethodParameters.MP.Menu;
using Microsoft.AspNetCore.Mvc;
using TokenHandler.Export;

namespace ApiRest.Controllers
{
    [ProjectTesisAuthorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsByOptionController : ControllerBase
    {
        public readonly IPermissionsByOptionBO _iPermissionsByOptionBO;
        public PermissionsByOptionController(IPermissionsByOptionBO iPermissionsByOptionBO)
        {
            _iPermissionsByOptionBO = iPermissionsByOptionBO;
        }

        [HttpPost("GetAll")]
        public IActionResult GetAll(GetPermissionsByOptionIn getPermissionsByOptionIn)
        {
            return Ok(_iPermissionsByOptionBO.GetAll(getPermissionsByOptionIn));
        }

    }
}