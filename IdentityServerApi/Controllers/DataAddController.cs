using Duende.IdentityServer.EntityFramework.DbContexts;
using EntityLayer.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace IdentityServerApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DataAddController : BaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly ConfigurationDbContext _configurationDbContext;



        public DataAddController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, ConfigurationDbContext configurationDbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configurationDbContext = configurationDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> DataAddBro()
        {
           await DataAdd.AddData(_userManager,_roleManager);
            return Ok();
        }

        [HttpGet]
        public  IActionResult IdentityServerDataAdd()
        {
            DataSeed.ConfigureDbSeed(_configurationDbContext);
            return Ok();
        }
        [HttpGet]
        public IActionResult error()
        {
            throw new DBConcurrencyException();
        }
    }
}
