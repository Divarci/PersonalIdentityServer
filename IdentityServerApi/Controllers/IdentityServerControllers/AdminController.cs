using EntityLayer.Models.DTOs.AuthenticationDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Constants;
using ServiceLayer.Services.AdminServices;

namespace IdentityServerApi.Controllers.IdentityServerControllers
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
            var userList = await _adminService.GetUsersAsync();
            return CreateAction(userList);
        }

        [HttpGet]
        public async Task<IActionResult> GetUser(string userId)
        {
            var result = await _adminService.GetUserByIdAsync(userId);
            return CreateAction(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserByAdmin(UserUpdateDtoForAdmin request)
        {
            var result = await _adminService.UserUpdateByAdminAsync(request);
            return CreateAction(result);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveUserByAdmin(string userId)
        {
            var result = await _adminService.RemoveUserAsync(userId);
            return CreateAction(result);
        }

    }
}
