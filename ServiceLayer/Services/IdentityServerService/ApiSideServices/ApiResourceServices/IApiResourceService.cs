using EntityLayer.Models.DTOs.ApiSideDto.ApiResourceDto;
using EntityLayer.Models.ResponseModels;

namespace ServiceLayer.Services.IdentityServerService.ApiSideServices.ApiResourceServices
{
    public interface IApiResourceService
    {
        Task<CustomResponseDto<List<ApiResourceItemDto>>> GetAllApiResourcesAsync();
        Task<CustomResponseDto<ApiResourceItemDto>> GetApiResourceByIdAsync(int id);
        Task<CustomResponseDto<ApiResourceCreateDto>> AddApiResourceAsync(ApiResourceCreateDto request);
        Task<CustomResponseDto<NoContentDto>> UpdateApiResourceAsync(ApiResourceUpdateDto request);
        Task<CustomResponseDto<NoContentDto>> RemoveApiResourceAsync(int id);

    }
}
