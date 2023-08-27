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
    public class UserTypeController : ControllerBase
    {
        public readonly IUserTypeBO _iUserTypeBO;
        public UserTypeController(IUserTypeBO iUserTypeBO)
        {
            _iUserTypeBO = iUserTypeBO;
        }

        [HttpPost("GetAll")]
        public IActionResult GetAll(GetUserTypeIn getUserTypeIn)
        {
            return Ok(_iUserTypeBO.GetAll(getUserTypeIn));
        }

        [HttpPost("Add")]
        public IActionResult Add(AddUserTypeIn addUserTypeIn)
        {
            return Ok(_iUserTypeBO.Add(addUserTypeIn));
        }

        [HttpPost("Update")]
        public IActionResult Update(UpdateUserTypeIn updateUserTypeIn)
        {
            return Ok(_iUserTypeBO.Update(updateUserTypeIn));
        }
        [HttpPost("Delete")]
        public IActionResult Delete(UpdateUserTypeIn updateUserTypeIn)
        {
            return Ok(_iUserTypeBO.Delete(updateUserTypeIn));
        }
    }
}