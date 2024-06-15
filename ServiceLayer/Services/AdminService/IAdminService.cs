using EntityLayer.Models.DTOs;
using EntityLayer.Models.DTOs.ClientDto;
using EntityLayer.Models.ResponseModels;
using Microsoft.AspNetCore.Http;

namespace ServiceLayer.Services.AdminService
{
    public interface IAdminService
    {      
        Task<CustomResponseDto<List<UserDtoForAdmin>>> GetUsersAsync();
        Task<CustomResponseDto<UserUpdateDtoForAdmin>> GetUserByIdAsync(string userId);
        Task<CustomResponseDto<NoContentDto>> UserUpdateByAdminAsync(UserUpdateDtoForAdmin request);
        Task<CustomResponseDto<NoContentDto>> RemoveUserAsync(string id);
    }
}
