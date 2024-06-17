using EntityLayer.Models.DTOs.ClientDto.ClientGrantTypeDto;
using EntityLayer.Models.DTOs.ClientDto.ClientScopeDto;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.IdentityServerService.ClientServices.ClientScopeServices;

namespace IdentityServerApi.Controllers.IdentityServerConfigControllers.ClientControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClientScopeController : BaseController
    {
        private readonly IClientScopeService _clientScopeService;

        public ClientScopeController(IClientScopeService clientScopeService)
        {
            _clientScopeService = clientScopeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClientScopes()
        {
            var result = await _clientScopeService.GetAllClientScopesAsync();
            return CreateAction(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientScopesById(int id)
        {
            var result = await _clientScopeService.GetClientScopeByIdAsync(id);
            return CreateAction(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddClientScope(ClientScopeCreateDto request)
        {
            var result = await _clientScopeService.CreateClientScopeAsync(request);
            return CreateAction(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateClientScope(ClientScopeUpdateDto request)
        {
            var result = await _clientScopeService.UpdateClientScopeAsync(request);
            return CreateAction(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveClientScope(int id)
        {
            var result = await _clientScopeService.RemoveClientScopeAsync(id);
            return CreateAction(result);
        }
    }
}
