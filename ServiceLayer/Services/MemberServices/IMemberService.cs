using EntityLayer.Models.DTOs.AuthenticationDto;
using EntityLayer.Models.ResponseModels;
using System.Security.Claims;

namespace ServiceLayer.Services.MemberServices
{
    public interface IMemberService
    {
        Task<CustomResponseDto<UserDtoForMember>> GetUserAsync(ClaimsPrincipal contextUser);
        Task<CustomResponseDto<NoContentDto>> UserUpdateAsync(UserDtoForMember request, ClaimsPrincipal contextUser);
        Task<CustomResponseDto<NoContentDto>> PasswordChangeAsync(PasswordUpdateDto request, ClaimsPrincipal contextUser);
    }


}
