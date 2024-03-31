using AutoMapper;
using Duende.IdentityServer.Extensions;
using EntityLayer.Messages;
using EntityLayer.Models.DTOs;
using EntityLayer.Models.Entities;
using EntityLayer.Models.ResponseModels;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ServiceLayer.Services.MemberService
{
    public class MemberService : IMemberService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public MemberService(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<UserDtoForMember>> GetUserAsync(ClaimsPrincipal contextUser)
        {
            var user = await _userManager.FindByIdAsync(contextUser.Identity.GetSubjectId());
            if (user == null)
            {
                return CustomResponseDto<UserDtoForMember>.Fail(404, new ErrorDto(CustomErrorMessages.PasswordNotMatch));
            }
            var mappedUser = _mapper.Map<UserDtoForMember>(user);
            return CustomResponseDto<UserDtoForMember>.Success(mappedUser, 200);
        }

        public async Task<CustomResponseDto<NoContentDto>> PasswordChangeAsync(PasswordUpdateDto request, ClaimsPrincipal contextUser)
        {
            var user = await _userManager.FindByIdAsync(contextUser.Identity.GetSubjectId());

            if (user == null)
            {
                return CustomResponseDto<NoContentDto>.Fail(404, new ErrorDto(CustomErrorMessages.PasswordNotMatch));
            }

            var resultPasswordMatch = request.Password == request.PasswordConfirm ? true : false;
            if (!resultPasswordMatch)
            {
                return CustomResponseDto<NoContentDto>.Fail(404, new ErrorDto(CustomErrorMessages.PasswordNotMatch));
            }

            var resultPasswordChange = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.Password);
            if (!resultPasswordChange.Succeeded)
            {
                var errorDto = new ErrorDto(new List<string>());
                foreach (var error in resultPasswordChange.Errors)
                {
                    errorDto.Errors!.Add(error.Description);
                }
                return CustomResponseDto<NoContentDto>.Fail(404, errorDto);

            }

            return CustomResponseDto<NoContentDto>.Success(204);

        }

        public async Task<CustomResponseDto<NoContentDto>> UserUpdateAsync(UserDtoForMember request, ClaimsPrincipal contextUser)
        {
            var user = await _userManager.FindByIdAsync(contextUser.Identity.GetSubjectId());
            if (user == null)
            {
                return CustomResponseDto<NoContentDto>.Fail(404, new ErrorDto(CustomErrorMessages.UserNotExist));
            }

            var mappedUser = _mapper.Map(request, user);
            var result = await _userManager.UpdateAsync(mappedUser);
            if (!result.Succeeded)
            {
                var errorDto = new ErrorDto(new List<string>());
                foreach (var error in result.Errors)
                {
                    errorDto.Errors!.Add(error.Description);
                }
                return CustomResponseDto<NoContentDto>.Fail(404, errorDto);
            }

            return CustomResponseDto<NoContentDto>.Success(204);
        }
    }
}
