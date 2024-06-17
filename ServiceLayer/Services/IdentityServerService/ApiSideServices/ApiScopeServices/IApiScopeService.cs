using EntityLayer.Models.DTOs.ApiSideDto.ApiScopeDto;
using EntityLayer.Models.ResponseModels;

namespace ServiceLayer.Services.IdentityServerService.ApiSideServices.ApiScopeServices
{
    public interface IApiScopeService
    {
        Task<CustomResponseDto<List<ApiScopeItemDto>>> GetAllApiScopesAsync();
        Task<CustomResponseDto<ApiScopeItemDto>> GetApiScopeByIdAsync(int id);
        Task<CustomResponseDto<ApiScopeCreateDto>> AddApiScopeAsync(ApiScopeCreateDto request);
        Task<CustomResponseDto<NoContentDto>> UpdateApiScopeAsync(ApiScopeUpdateDto request);
        Task<CustomResponseDto<NoContentDto>> RemoveApiScopeAsync(int id);

    }
}
