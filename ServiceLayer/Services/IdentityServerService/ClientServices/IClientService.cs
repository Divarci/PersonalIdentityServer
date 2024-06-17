using EntityLayer.Models.DTOs.ClientDto;
using EntityLayer.Models.ResponseModels;

namespace ServiceLayer.Services.IdentityServerService.ClientServices
{
    public interface IClientService
    {
        Task<CustomResponseDto<ClientCreateDto>> CreateClientAsync(ClientCreateDto request);
        Task<CustomResponseDto<NoContentDto>> UpdateClientAsync(ClientUpdateDto request);
        Task<CustomResponseDto<ClientItemDto>> GetClientByIdAsync(int id);
        Task<CustomResponseDto<List<ClientItemDto>>> GetAllClients();
        Task<CustomResponseDto<NoContentDto>> RemoveClientAsync(int id);
    }
}
