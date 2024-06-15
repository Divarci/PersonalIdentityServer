using EntityLayer.Models.DTOs.ClientDto;
using EntityLayer.Models.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Constants;
using ServiceLayer.Services.IdentityServerService;

namespace IdentityServerApi.Controllers
{
    [Authorize(CustomIdentityConstants.AdminRole)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IdentityServerController : BaseController
    {
        private readonly IIdentityServerService _identityServerService;

        public IdentityServerController(IIdentityServerService identityServerService)
        {
            _identityServerService = identityServerService;
        }

        #region CLIENT

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

        #endregion

        #region SCOPES
        #endregion

        #region GRANT-TYPES
        #endregion

        #region SECRETS
        #endregion
    }
}
