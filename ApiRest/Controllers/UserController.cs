using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using MethodParameters.MP;
using TokenHandler.Export;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiRest.Controllers
{
    [ProjectTesisAuthorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserBO _iUserBO;
        public UserController(IUserBO iUserBO)
        {
            _iUserBO = iUserBO;
        }

        [HttpPost("GetAll")]
        public IActionResult GetAll(GetAllUserIn getAllUserOut)
        {
            return Ok(_iUserBO.GetAll(getAllUserOut));
        }

        [HttpGet("Get")]
        public IActionResult Get()
        {
            return Ok(_iUserBO.GetAll());
        }

        [HttpPost("Add")]
        public IActionResult Add(AddUserIn addUserIn)
        {
            return Ok(_iUserBO.Add(addUserIn));
        }

        [HttpPost("Update")]
        public IActionResult Update(UpdateUserIn updateUserIn)
        {
            return Ok(_iUserBO.Update(updateUserIn));
        }

        //[HttpPost("UpdatePassword")]
        //public IActionResult UpdatePassword(UpdateUserIn updateUserIn)
        //{
        //    return Ok(_iUserBO.UpdatePassword(updateUserIn));
        //}

        [HttpPost("Delete")]
        public IActionResult Delete(UpdateUserIn updateUserIn)
        {
            return Ok(_iUserBO.Delete(updateUserIn));
        }
        //[HttpPost("ValidateRecoverPassword")]
        //public IActionResult ValidateRecoverPassword(ValidateRecoverPasswordIn validateRecoverPasswordIn)
        //{
        //    return Ok(_iUserBO.ValidateRecoverPassword(validateRecoverPasswordIn));
        //}
    }
}