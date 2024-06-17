using Azure.Core;
using EntityLayer.Models.DTOs.ClientDto.ClientGrantTypeDto;
using EntityLayer.Models.DTOs.ClientDto.ClientSecretDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.IdentityServerService.ClientServices.ClientGrantTypeServices;

namespace IdentityServerApi.Controllers.IdentityServerConfigControllers.ClientControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClientGrantTypeController : BaseController
    {
        private readonly IClientGrantTypeService _clientGrantTypeService;

        public ClientGrantTypeController(IClientGrantTypeService clientGrantTypeService)
        {
            _clientGrantTypeService = clientGrantTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGrantTypes()
        {
            var result = await _clientGrantTypeService.GetAllClientGrantTypesAsync();
            return CreateAction(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGrantTypeById(int id)
        {
            var result = await _clientGrantTypeService.GetClientGrantTypeByIdAsync(id);
            return CreateAction(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddClientGrantType(ClientGrantTypeCreateDto request)
        {
            var result = await _clientGrantTypeService.CreateClientGrantTypeAsync(request);
            return CreateAction(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateClientGrantType(ClientGrantTypeUpdateDto request)
        {
            var result = await _clientGrantTypeService.UpdateClientGrantTypeAsync(request);
            return CreateAction(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveClientGrantType(int id)
        {
            var result = await _clientGrantTypeService.RemoveClientGrantTypeAsync(id);
            return CreateAction(result);
        }
    }
}
