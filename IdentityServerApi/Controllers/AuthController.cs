using EntityLayer.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.AuthService;
using static Duende.IdentityServer.IdentityServerConstants;

namespace IdentityServerApi.Controllers
{
    [Authorize(LocalApi.PolicyName)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto request)
        {

            var result = await _authService.RegisterAsync(request, HttpContext);
            return CreateAction(result);
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordInfo info)
        {
            var result = await _authService.ForgotPasswordAsync(info.ForgotPasswordConnection, info.EmailService, HttpContext);
            return CreateAction(result);
        }

        [HttpPut]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto request)
        {

            var result = await _authService.ResetPasswordAsync(request);
            return CreateAction(result);
        }

    }
}
