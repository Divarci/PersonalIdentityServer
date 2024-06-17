using EntityLayer.Models.DTOs.ClientDto.ClientScopeDto;
using EntityLayer.Models.ResponseModels;

namespace ServiceLayer.Services.IdentityServerService.ClientServices.ClientScopeServices
{
    public interface IClientScopeService
    {
        Task<CustomResponseDto<List<ClientScopeItemDto>>> GetAllClientScopesAsync();
        Task<CustomResponseDto<ClientScopeItemDto>> GetClientScopeByIdAsync(int id);
        Task<CustomResponseDto<ClientScopeCreateDto>> CreateClientScopeAsync(ClientScopeCreateDto request);
        Task<CustomResponseDto<NoContentDto>> UpdateClientScopeAsync(ClientScopeUpdateDto request);
        Task<CustomResponseDto<NoContentDto>> RemoveClientScopeAsync(int id);
    }
}
