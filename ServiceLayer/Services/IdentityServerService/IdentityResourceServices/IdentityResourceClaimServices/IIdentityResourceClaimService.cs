using EntityLayer.Models.DTOs.IdentityResourceDto.IdentityResourceClaimDto;
using EntityLayer.Models.ResponseModels;

namespace ServiceLayer.Services.IdentityServerService.IdentityResourceServices.IdentityResourceClaimServices
{
    public interface IIdentityResourceClaimService
    {
        Task<CustomResponseDto<List<IdentityResourceClaimItemDto>>> GetAllIdentityResourceClaimsAsync();
        Task<CustomResponseDto<IdentityResourceClaimItemDto>> GetIdentityResourceClaimByIdAsync(int id);
        Task<CustomResponseDto<IdentityResourceClaimCreateDto>> CerateIdentityResourceClaimAsync(IdentityResourceClaimCreateDto request);
        Task<CustomResponseDto<NoContentDto>> UpdateIdentityResourceClaimAsync(IdentityResourceClaimUpdateDto request);
        Task<CustomResponseDto<NoContentDto>> RemoveIdentityResourceClaimAsync(int id);
    }
}
