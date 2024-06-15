using EntityLayer.Models.DTOs;
using EntityLayer.Models.DTOs.ClientDto;
using EntityLayer.Models.ResponseModels;
using Microsoft.AspNetCore.Http;

namespace ServiceLayer.Services.AdminService
{
    public interface IAdminService
    {
        #region USER

        Task<CustomResponseDto<List<UserDtoForAdmin>>> GetUsersAsync();
        Task<CustomResponseDto<UserUpdateDtoForAdmin>> GetUserByIdAsync(string userId);
        Task<CustomResponseDto<NoContentDto>> UserUpdateByAdminAsync(UserUpdateDtoForAdmin request);
        Task<CustomResponseDto<NoContentDto>> RemoveUserAsync(string id);

        #endregion

        #region CLIENT
        Task<CustomResponseDto<ClientCreateDto>> CreateClientAsync(ClientCreateDto request);
        Task<CustomResponseDto<NoContentDto>> UpdateClientAsync(ClientUpdateDto request);
        Task<CustomResponseDto<ClientDto>> GetClientByIdAsync(int id);
        Task<CustomResponseDto<List<ClientDto>>> GetAllClients();
        Task<CustomResponseDto<NoContentDto>> RemoveClientAsync(int id);

        #endregion

    }
}
