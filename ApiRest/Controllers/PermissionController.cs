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
    public class PermissionController : BaseController
    {
        public readonly IPermissionBO _iPermissionBO;
        public PermissionController(IPermissionBO iPermissionBO)
        {
            _iPermissionBO = iPermissionBO;
        }

        [HttpPost("GetAll")]
        public IActionResult GetAll(GetPermissionIn getPermissionIn)
        {
            return Ok(_iPermissionBO.GetAll(getPermissionIn));
        }

        [HttpPost("Add")]
        public IActionResult Add(AddPermissionIn addPermissionIn)
        {
            return Ok(_iPermissionBO.Add(addPermissionIn));
        }

        [HttpPost("Update")]
        public IActionResult Update(UpdatePermissionIn updatePermissionIn)
        {
            return Ok(_iPermissionBO.Update(updatePermissionIn));
        }

        [HttpPost("Delete")]
        public IActionResult Delete(UpdatePermissionIn updatePermissionIn)
        {
            return Ok(_iPermissionBO.Delete(updatePermissionIn));
        }
    }
}
