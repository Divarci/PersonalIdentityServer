using EntityLayer.Models.DTOs;
using EntityLayer.Models.ResponseModels;
using Microsoft.AspNetCore.Http;

namespace ServiceLayer.Services.AuthService
{
    public interface IAuthService
    {
        Task<CustomResponseDto<RegisterDto>> RegisterAsync(RegisterDto request, HttpContext httpContext);
        Task<CustomResponseDto<NoContentDto>> ForgotPasswordAsync(ForgotPasswordConnection request, HttpContext context);
        Task<CustomResponseDto<NoContentDto>> ResetPasswordAsync(ResetPasswordDto request);
    }
}
