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
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserBO _iUserBO;
        private readonly IMapper _mapper;
        private readonly IAuthService _iAuthService;
        private readonly IConfiguration _Configuration;

        public AuthController(IUserBO iUserBO, IAuthService iAuthService, IMapper mapper, IConfiguration Configuration)
        {
            _iUserBO = iUserBO;
            _iAuthService = iAuthService;
            _mapper = mapper;
            _Configuration = Configuration;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginIn loginIn)
        {
            if (ModelState.IsValid)
            {
                LoginOut resultLogin = _iAuthService.ValidateLogin(loginIn);
                if (resultLogin != null && resultLogin.IsLoginOk)
                {
                    return Ok(_iAuthService.GenerateToken(resultLogin));
                }
                else
                {
                    return Ok(new AuthenticationOut()
                    {
                        IsActive = false,
                        Result = Result.Success,
                        IsAuthetnicated = false,
                    }); ;
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        [ProjectTesisAuthorize]
        //[Authorize]
        [HttpPost("UserInformation")]
        public IActionResult InformationToken()
        {
            return Ok(_iUserBO.GetAll(new GetAllUserIn()));
        }

        [HttpPost("ValidateRecoverPassword")]
        public IActionResult ValidateRecoverPassword([FromBody] ValidateRecoverPasswordIn validateRecoverPasswordIn)
        {
            return Ok(_iUserBO.ValidateRecoverPassword(validateRecoverPasswordIn));
        }

        [HttpPost("UpdatePassword")]
        public IActionResult UpdatePassword(UpdateUserIn updateUserIn)
        {
            return Ok(_iUserBO.UpdatePassword(updateUserIn));
        }


    }
}
