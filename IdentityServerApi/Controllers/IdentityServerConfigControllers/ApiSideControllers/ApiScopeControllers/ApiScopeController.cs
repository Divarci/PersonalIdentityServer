using EntityLayer.Models.DTOs.ApiSideDto.ApiScopeDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Constants;
using ServiceLayer.Services.IdentityServerService.ApiSideServices.ApiScopeServices;

namespace IdentityServerApi.Controllers.IdentityServerConfigControllers.ApiSideControllers.ApiScopeControllers
{
    [Authorize(CustomIdentityConstants.AdminRole)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApiScopeController : BaseController
    {
        private readonly IApiScopeService _scopeService;

        public ApiScopeController(IApiScopeService scopeService)
        {
            _scopeService = scopeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllApiScopes()
        {
            var result = await _scopeService.GetAllApiScopesAsync();
            return CreateAction(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetApiScopeById(int id)
        {
            var result = await _scopeService.GetApiScopeByIdAsync(id);
            return CreateAction(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddScope(ApiScopeCreateDto request)
        {
            var result = await _scopeService.AddApiScopeAsync(request);
            return CreateAction(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ApiScopeUpdateDto request)
        {
            var result = await _scopeService.UpdateApiScopeAsync(request);
            return CreateAction(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveApiScope(int id)
        {
            var result = await _scopeService.RemoveApiScopeAsync(id);
            return CreateAction(result);
        }
    }
}
