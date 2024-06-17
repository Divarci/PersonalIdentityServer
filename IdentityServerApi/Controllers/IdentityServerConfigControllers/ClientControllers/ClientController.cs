using EntityLayer.Models.DTOs.ClientDto;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.IdentityServerService.ClientServices;

namespace IdentityServerApi.Controllers.IdentityServerConfigControllers.ClientControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClientController : BaseController
    {
        private readonly IClientService _identityServerService;

        public ClientController(IClientService identityServerService)
        {
            _identityServerService = identityServerService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            var result = await _identityServerService.GetAllClients();
            return CreateAction(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddClient(ClientCreateDto request)
        {
            var result = await _identityServerService.CreateClientAsync(request);
            return CreateAction(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById(int id)
        {
            var result = await _identityServerService.GetClientByIdAsync(id);
            return CreateAction(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateClient(ClientUpdateDto request)
        {
            var result = await _identityServerService.UpdateClientAsync(request);
            return CreateAction(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveClient(int id)
        {
            var result = await _identityServerService.RemoveClientAsync(id);
            return CreateAction(result);
        }
    }
}
