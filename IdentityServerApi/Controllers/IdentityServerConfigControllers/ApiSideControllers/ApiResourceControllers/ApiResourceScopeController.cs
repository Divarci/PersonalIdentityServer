using EntityLayer.Models.DTOs.ApiSideDto.ApiResourceDto.ApiResourceScopeDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Constants;
using ServiceLayer.Services.IdentityServerService.ApiSideServices.ApiResourceScopeServices;

namespace IdentityServerApi.Controllers.IdentityServerConfigControllers.ApiSideControllers.ApiResourceControllers
{
    [Authorize(CustomIdentityConstants.AdminRole)]
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class ApiResourceScopeController : BaseController
    {
        private readonly IApiResourceScopeService _apiResourceScopeService;

        public ApiResourceScopeController(IApiResourceScopeService apiResourceScopeService)
        {
            _apiResourceScopeService = apiResourceScopeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllApiResourceScopes()
        {
            var result = await _apiResourceScopeService.GetAllApiResourceScopeListAsync();
            return CreateAction(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetApiResourceScopeById(int id)
        {
            var result = await _apiResourceScopeService.GetApiResourceScopeByIdAsync(id);
            return CreateAction(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddApiResourceScope(ApiResourceScopeCreateDto request)
        {
            var result = await _apiResourceScopeService.CreateApiResourceScopeAsync(request);
            return CreateAction(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateApiResourceScope(ApiResourceScopeUpdateDto request)
        {
            var result = await _apiResourceScopeService.UpdateApiResourceScopeAsync(request);
            return CreateAction(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveApiResourceDto(int id)
        {
            var result = await _apiResourceScopeService.RemoveApiResourceScopeAsync(id);
            return CreateAction(result);
        }
    }
}
