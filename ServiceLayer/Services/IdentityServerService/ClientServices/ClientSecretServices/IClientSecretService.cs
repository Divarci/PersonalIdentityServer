using EntityLayer.Models.DTOs.ClientDto.ClientSecretDto;
using EntityLayer.Models.ResponseModels;

namespace ServiceLayer.Services.IdentityServerService.ClientServices.ClientSecretServices
{
    public interface IClientSecretService
    {
        Task<CustomResponseDto<List<ClientSecretItemDto>>> GetAllClientSecretsAsync();
        Task<CustomResponseDto<ClientSecretItemDto>> GetClientSecretByIdAsync(int id);
        Task<CustomResponseDto<ClientSecretCreateDto>> CreateClientSecretAsync(ClientSecretCreateDto request);
        Task<CustomResponseDto<NoContentDto>> UpdateClientSecretAsync(ClientSecretUpdateDto request);
        Task<CustomResponseDto<NoContentDto>> RemoveClientSecretAsync(int id);
    }
}
