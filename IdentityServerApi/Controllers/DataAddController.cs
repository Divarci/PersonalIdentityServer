using EntityLayer.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServerApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DataAddController : BaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;


        public DataAddController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> DataAddBro()
        {
           await DataAdd.AddData(_userManager,_roleManager);
            return Ok();
        }
    }
}
