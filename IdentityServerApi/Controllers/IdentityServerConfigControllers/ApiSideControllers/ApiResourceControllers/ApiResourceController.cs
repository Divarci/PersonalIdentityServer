using EntityLayer.Models.DTOs.ApiSideDto.ApiResourceDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Constants;
using ServiceLayer.Services.IdentityServerService.ApiSideServices.ApiResourceServices;

namespace IdentityServerApi.Controllers.IdentityServerConfigControllers.ApiSideControllers.ApiResourceControllers
{
    [Authorize(CustomIdentityConstants.AdminRole)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApiResourceController : BaseController
    {
        private readonly IApiResourceService _apiResourceService;

        public ApiResourceController(IApiResourceService apiResourceService)
        {
            _apiResourceService = apiResourceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllApiResources()
        {
            var result = await _apiResourceService.GetAllApiResourcesAsync();
            return CreateAction(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetApiResourceById(int id)
        {
            var result = await _apiResourceService.GetApiResourceByIdAsync(id);
            return CreateAction(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddApiResources(ApiResourceCreateDto request)
        {
            var result = await _apiResourceService.AddApiResourceAsync(request);
            return CreateAction(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateApiResources(ApiResourceUpdateDto request)
        {
            var result = await _apiResourceService.UpdateApiResourceAsync(request);
            return CreateAction(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveApiResource(int id)
        {
            var result = await _apiResourceService.RemoveApiResourceAsync(id);
            return CreateAction(result);
        }
    }
}
