using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    public class OptionController : ControllerBase
    {
        public readonly IOptionBO _iOptionBO;
        public readonly ICompanyBO _iCompanyBO;
        public OptionController(IOptionBO iOptionBO, ICompanyBO iCompanyBO)
        {
            _iOptionBO = iOptionBO;
            _iCompanyBO = iCompanyBO;
        }

        [HttpPost("GetByUser")]
        public IActionResult GetByUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            string profileId = claim.FirstOrDefault(x => x.Type.Equals("ProfileId")).Value;
            string IsSuperAdmin = claim.FirstOrDefault(x => x.Type.Equals("IsSuperAdmin")).Value;
            int companyId = Convert.ToInt32(claim.FirstOrDefault(x => x.Type.Equals("CompanyId")).Value);

            GetAllOptionMenuOut getAllOptionMenuOut = _iOptionBO.GetAllMenu(new GetAllOptionIn() { ProfileId = Convert.ToInt32(profileId) });
            GetAllCompanyOut getAllCompanyOut = _iCompanyBO.GetAll(new GetAllCompanyIn()
            {
                Company = new MethodParameters.VM.CompanyVM()
                {
                    Id = companyId
                }
            });

            if (getAllCompanyOut != null && getAllCompanyOut.ListCompany != null && getAllCompanyOut.ListCompany.Any())
            {
                getAllOptionMenuOut.LogoCompany = getAllCompanyOut.ListCompany.First().ImageLogo;
            }

            if (!string.IsNullOrEmpty(IsSuperAdmin))
                getAllOptionMenuOut.IsSuperAdmin = Convert.ToBoolean(IsSuperAdmin);

            getAllOptionMenuOut.FullName = claim.FirstOrDefault(x => x.Type.Equals("FullName")).Value;

            return Ok(getAllOptionMenuOut);
        }

        [HttpPost("GetAll")]
        public IActionResult GetAll(GetAllOptionIn getAllOptionIn)
        {
            return Ok(_iOptionBO.GetAll(getAllOptionIn));
        }

        [HttpPost("GetAllMenu")]
        public IActionResult GetAllMenu(GetAllOptionIn getAllOptionIn)
        {
            return Ok(_iOptionBO.GetAllMenu(getAllOptionIn));
        }

        [HttpPost("Add")]
        public IActionResult Add(AddOptionIn addOptionIn)
        {
            return Ok(_iOptionBO.Add(addOptionIn));
        }

        [HttpPost("Update")]
        public IActionResult Update(UpdateOptionIn updateOptionIn)
        {
            return Ok(_iOptionBO.Update(updateOptionIn));
        }

        [HttpPost("AddMenu")]
        public IActionResult AddMenu(AddOptionMenuIn addOptionMenuIn)
        {
            return Ok(_iOptionBO.AddMenu(addOptionMenuIn));
        }
        //[HttpPost("MenuPermissions")]
        //public IActionResult MenuPermissions(GetAllOptionIn getAllOptionIn)
        //{
        //    return Ok(_iOptionBO.GetAllMenuWithPermission(getAllOptionIn));
        //}
    }
}
