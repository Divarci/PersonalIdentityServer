using EntityLayer.Models.DTOs;
using EntityLayer.Models.DTOs.ClientDto;
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

        [HttpPost]
        public async Task<IActionResult> AddClient(ClientCreateDto request)
        {
            var result = await _adminService.CreateClientAsync(request);
            return CreateAction(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById(int id)
        {
            var result = await _adminService.GetClientByIdAsync(id);
            return CreateAction(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateClient(ClientUpdateDto request)
        {
            var result = await _adminService.UpdateClientAsync(request);
            return CreateAction(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveClient(int id)
        {
            var result = await _adminService.RemoveClientAsync(id);
            return CreateAction(result);
        }
    }
}
