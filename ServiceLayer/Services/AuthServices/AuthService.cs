using AutoMapper;
using EntityLayer.Messages;
using EntityLayer.Models.DTOs.AuthenticationDto;
using EntityLayer.Models.Entities;
using EntityLayer.Models.ResponseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ServiceLayer.Constants;
using ServiceLayer.Helpers.EmailSender;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ServiceLayer.Services.AuthServices
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

        public async Task<CustomResponseDto<NoContentDto>> RegisterAsync(RegisterDto request, HttpContext httpContext)
        {
            var user = _mapper.Map<AppUser>(request);

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return CustomResponseDto<NoContentDto>.Fail(404, new ErrorDto(errors));
            }

            await _userManager.AddToRoleAsync(user, request.Role);


            return CustomResponseDto<NoContentDto>.Success(201);
        }

        public async Task<CustomResponseDto<NoContentDto>> ForgotPasswordAsync(ForgotPasswordConnection connection, EmailServiceInfo emailService, HttpContext context)
        {
            var user = await _userManager.FindByEmailAsync(connection.Email);

            if (user == null)
            {
                return CustomResponseDto<NoContentDto>.Fail(404, new ErrorDto("User does not exist!"));
            }

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            var passwordResetLink = $"{connection.Url}?userId={user.Id}&token={resetToken}";

            await _emailSender.SendEmailWithTokenForResetPasswordAsync(connection.Email, passwordResetLink, emailService);

            return CustomResponseDto<NoContentDto>.Success(201);


        }

        public async Task<CustomResponseDto<NoContentDto>> ResetPasswordAsync(ResetPasswordDto request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return CustomResponseDto<NoContentDto>.Fail(404, CustomErrorMessages.UserNotExist);
            }


            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return CustomResponseDto<NoContentDto>.Fail(404, new ErrorDto(errors));
            }

            return CustomResponseDto<NoContentDto>.Success(204);

        }
    }
}
