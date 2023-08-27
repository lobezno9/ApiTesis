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
    public class OptionsByProfileController : ControllerBase
    {
        public readonly IOptionsByProfileBO _iOptionsByProfileBO;
        public OptionsByProfileController(IOptionsByProfileBO iOptionsByProfileBO)
        {
            _iOptionsByProfileBO = iOptionsByProfileBO;
        }

        [HttpPost("GetAll")]
        public IActionResult GetAll(GetAllOptionsByProfileIn getAllOptionsByProfileIn)
        {
            return Ok(_iOptionsByProfileBO.GetAll(getAllOptionsByProfileIn));
        }
    }
}
