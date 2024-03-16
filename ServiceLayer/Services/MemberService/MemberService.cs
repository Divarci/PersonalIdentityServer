using AutoMapper;
using Duende.IdentityServer.Extensions;
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
                return CustomResponseDto<UserDtoForMember>.Fail(404, new ErrorDto("User does not exist!"));
            }
            var mappedUser = _mapper.Map<UserDtoForMember>(user);
            return CustomResponseDto<UserDtoForMember>.Success(mappedUser, 200);
        }

        public async Task<CustomResponseDto<NoContentDto>> PasswordChangeAsync(PasswordUpdateDto request, ClaimsPrincipal contextUser)
        {
            var user = await _userManager.FindByIdAsync(contextUser.Identity.GetSubjectId());

            if (user == null)
            {
                return CustomResponseDto<NoContentDto>.Fail(404, new ErrorDto("User does not exist!"));
            }

            var resultPasswordMatch = request.Password == request.PasswordConfirm ? true : false;
            if (!resultPasswordMatch)
            {
                return CustomResponseDto<NoContentDto>.Fail(404, new ErrorDto("Your password must be matched with confirm password!"));
            }

            var resultPasswordChange = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.Password);
            if (!resultPasswordChange.Succeeded)
            {
                if (!resultPasswordChange.Succeeded)
                {
                    if (!resultPasswordChange.Errors.Any())
                    {
                        return CustomResponseDto<NoContentDto>.Fail(404, "Unknown error while updating!");
                    }
                    var errorDto = new ErrorDto(new List<string>());
                    foreach (var error in resultPasswordChange.Errors)
                    {
                        errorDto.Errors!.Add(error.Description);
                    }
                    return CustomResponseDto<NoContentDto>.Fail(404, errorDto);
                }
            }

            return CustomResponseDto<NoContentDto>.Success(201);

        }

        public async Task<CustomResponseDto<NoContentDto>> UserUpdateAsync(UserDtoForMember request, ClaimsPrincipal contextUser)
        {
            var user = await _userManager.FindByIdAsync(contextUser.Identity.GetSubjectId());
            if (user == null)
            {
                return CustomResponseDto<NoContentDto>.Fail(404, new ErrorDto("User does not exist!"));
            }

            var mappedUser = _mapper.Map(request,user);
            var result = await _userManager.UpdateAsync(mappedUser);
            if (!result.Succeeded)
            {
                if (!result.Errors.Any())
                {
                    return CustomResponseDto<NoContentDto>.Fail(404, "Unknown error while updating!");
                }
                var errorDto = new ErrorDto(new List<string>());
                foreach (var error in result.Errors)
                {
                    errorDto.Errors!.Add(error.Description);
                }
                return CustomResponseDto<NoContentDto>.Fail(404, errorDto);
            }

            return CustomResponseDto<NoContentDto>.Success(201);
        }
    }
}
