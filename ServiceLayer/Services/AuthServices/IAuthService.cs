using EntityLayer.Models.DTOs.AuthenticationDto;
using EntityLayer.Models.ResponseModels;
using Microsoft.AspNetCore.Http;

namespace ServiceLayer.Services.AuthServices
{
    public interface IAuthService
    {
        Task<CustomResponseDto<NoContentDto>> RegisterAsync(RegisterDto request, HttpContext httpContext);
        Task<CustomResponseDto<NoContentDto>> ForgotPasswordAsync(ForgotPasswordConnection connection, EmailServiceInfo emailService, HttpContext context);
        Task<CustomResponseDto<NoContentDto>> ResetPasswordAsync(ResetPasswordDto request);
    }
}
