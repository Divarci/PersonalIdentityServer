using EntityLayer.Models.DTOs.ClientDto.ClientGrantTypeDto;
using EntityLayer.Models.ResponseModels;

namespace ServiceLayer.Services.IdentityServerService.ClientServices.ClientGrantTypeServices
{
    public interface IClientGrantTypeService
    {
        Task<CustomResponseDto<List<ClientGrantTypeItemDto>>> GetAllClientGrantTypesAsync();
        Task<CustomResponseDto<ClientGrantTypeItemDto>> GetClientGrantTypeByIdAsync(int id);
        Task<CustomResponseDto<ClientGrantTypeCreateDto>> CreateClientGrantTypeAsync(ClientGrantTypeCreateDto request);
        Task<CustomResponseDto<NoContentDto>> UpdateClientGrantTypeAsync(ClientGrantTypeUpdateDto request);
        Task<CustomResponseDto<NoContentDto>> RemoveClientGrantTypeAsync(int id);
        
    }
}
