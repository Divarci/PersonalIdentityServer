using EntityLayer.Models.DTOs.ApiSideDto.ApiResourceDto.ApiResourceScopeDto;
using EntityLayer.Models.ResponseModels;

namespace ServiceLayer.Services.IdentityServerService.ApiSideServices.ApiResourceScopeServices
{
    public interface IApiResourceScopeService
    {
        Task<CustomResponseDto<List<ApiResourceScopeItemDto>>> GetAllApiResourceScopeListAsync();
        Task<CustomResponseDto<ApiResourceScopeItemDto>> GetApiResourceScopeByIdAsync(int id);
        Task<CustomResponseDto<ApiResourceScopeCreateDto>> CreateApiResourceScopeAsync(ApiResourceScopeCreateDto request);
        Task<CustomResponseDto<NoContentDto>> UpdateApiResourceScopeAsync(ApiResourceScopeUpdateDto request);
        Task<CustomResponseDto<NoContentDto>> RemoveApiResourceScopeAsync(int id);
    }
}
