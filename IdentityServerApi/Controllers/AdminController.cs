using EntityLayer.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Constants;
using ServiceLayer.Services.AdminService;

namespace IdentityServerApi.Controllers
{
    [Authorize(CustomIdentityConstants.AdminRole)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : BaseController
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetUserList()
        {
            var userList = await _adminService.GetUsersWithClientIdAsync(HttpContext);
            return CreateAction(userList);
        }

        [HttpGet]
        public async Task<IActionResult> GetUser(string userId)
        {
            var result = await _adminService.GetUserWithClientIdAsync(userId);
            return CreateAction(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserByAdmin(UserDtoForAdmin request)
        {
            var result = await _adminService.UserUpdateByAdminAsync(request);
            return CreateAction(result);
        }

    }
}
