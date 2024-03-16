using AutoMapper;
using EntityLayer.Models.DTOs;
using EntityLayer.Models.Entities;
using EntityLayer.Models.ResponseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ServiceLayer.Helpers.EmailSender;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ServiceLayer.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IEmailHelper _emailSender;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public AuthService(UserManager<AppUser> userManager, IMapper mapper, IEmailHelper emailSender)
        {
            _userManager = userManager;
            _mapper = mapper;
            _emailSender = emailSender;
        }

        public async Task<CustomResponseDto<RegisterDto>> RegisterAsync(RegisterDto request, HttpContext httpContext)
        {
            var clientId = httpContext.User.Claims.First(x => x.Type == "client_id").Value;

            var user = _mapper.Map<AppUser>(request);
            user.ClientId = clientId;

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return CustomResponseDto<RegisterDto>.Fail(404, new ErrorDto(errors));
            }
            return CustomResponseDto<RegisterDto>.Success(request, 200);
        }

        public async Task<CustomResponseDto<NoContentDto>> ForgotPasswordAsync(ForgotPasswordConnection request, HttpContext context)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return CustomResponseDto<NoContentDto>.Fail(404, new ErrorDto("User does not exist!"));
            }

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            var passwordResetLink = $"{context.Request.Scheme}://{request.Host}/{request.Controller}/{request.Action}?userId={user.Id}&token={resetToken}";

            await _emailSender.SendEmailWithTokenForResetPasswordAsync(request.Email, passwordResetLink);

            return CustomResponseDto<NoContentDto>.Success(201);


        }

        public async Task<CustomResponseDto<NoContentDto>> ResetPasswordAsync(ResetPasswordDto request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return CustomResponseDto<NoContentDto>.Fail(404, "User is not exist");
            }

            if (request.Password != request.PasswordConfirm)
            {
                return CustomResponseDto<NoContentDto>.Fail(404, "Passwaor and Password Confirm must be matched");

            }
            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return CustomResponseDto<NoContentDto>.Fail(404, new ErrorDto(errors));
            }

            return CustomResponseDto<NoContentDto>.Success(201);

        }
    }
}
