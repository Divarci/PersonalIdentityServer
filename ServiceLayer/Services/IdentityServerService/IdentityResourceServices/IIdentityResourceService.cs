using EntityLayer.Models.DTOs.IdentityResourceDto;
using EntityLayer.Models.ResponseModels;

namespace ServiceLayer.Services.IdentityServerService.IdentityResourceServices
{
    public interface IIdentityResourceService
    {
        Task<CustomResponseDto<IdentityResourceListDto>> GetAllIdentityResourcesAsync();
        Task<CustomResponseDto<IdentityResourceItemDto>> GetIdentityResourceByIdAsync(int id);
        Task<CustomResponseDto<IdentityResourceCreateDto>> CerateIdentityResourceAsync(IdentityResourceCreateDto request);
        Task<CustomResponseDto<NoContentDto>> UpdateIdentityResourceAsync(IdentityResourceUpdateDto request);
        Task<CustomResponseDto<NoContentDto>> RemoveIdentityResourceAsync(int id);
    }
}
