using EntityLayer.Models.DTOs.ClientDto;
using EntityLayer.Models.ResponseModels;

namespace ServiceLayer.Services.IdentityServerService
{
    public interface IIdentityServerService
    {
        #region CLIENT

        Task<CustomResponseDto<ClientCreateDto>> CreateClientAsync(ClientCreateDto request);
        Task<CustomResponseDto<NoContentDto>> UpdateClientAsync(ClientUpdateDto request);
        Task<CustomResponseDto<ClientDto>> GetClientByIdAsync(int id);
        Task<CustomResponseDto<List<ClientDto>>> GetAllClients();
        Task<CustomResponseDto<NoContentDto>> RemoveClientAsync(int id);

        #endregion
    }
}
