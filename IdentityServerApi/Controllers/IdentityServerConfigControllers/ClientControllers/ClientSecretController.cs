using EntityLayer.Models.DTOs.ClientDto.ClientScopeDto;
using EntityLayer.Models.DTOs.ClientDto.ClientSecretDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.IdentityServerService.ClientServices.ClientSecretServices;

namespace IdentityServerApi.Controllers.IdentityServerConfigControllers.ClientControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClientSecretController : BaseController
    {
        private readonly IClientSecretService _clientSecretService;

        public ClientSecretController(IClientSecretService clientSecretService)
        {
            _clientSecretService = clientSecretService;
        }

        [HttpPost]
        public async Task<IActionResult> AddClientSecret(ClientSecretCreateDto request)
        {
            var result = await _clientSecretService.CreateClientSecretAsync(request);
            return CreateAction(result);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateClientSecret(ClientSecretUpdateDto request)
        {
            var result = await _clientSecretService.UpdateClientSecretAsync(request);
            return CreateAction(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveClientSecret(int id)
        {
            var result = await _clientSecretService.RemoveClientSecretAsync(id);
            return CreateAction(result);
        }
    }
}
