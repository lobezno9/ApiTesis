using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using MethodParameters.MP;
using Microsoft.AspNetCore.Mvc;
using TokenHandler.Export;

namespace ApiRest.Controllers
{
    [ProjectTesisAuthorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        public readonly IProfileBO _iProfileBO;
        public ProfileController(IProfileBO iProfileBO)
        {
            _iProfileBO = iProfileBO;
        }

        [HttpPost("GetAll")]
        public IActionResult GetAll(GetAllProfileIn getAllProfileIn)
        {
            return Ok(_iProfileBO.GetAll(getAllProfileIn));
        }

        [HttpPost("Add")]
        public IActionResult Add(AddProfileIn addProfileIn)
        {
            return Ok(_iProfileBO.Add(addProfileIn));
        }

        [HttpPost("Update")]
        public IActionResult Update(UpdateProfileIn updateProfileIn)
        {
            return Ok(_iProfileBO.Update(updateProfileIn));
        }
        [HttpPost("Delete")]
        public IActionResult Delete(UpdateProfileIn updateProfileIn)
        {
            return Ok(_iProfileBO.Delete(updateProfileIn));
        }
    }
}