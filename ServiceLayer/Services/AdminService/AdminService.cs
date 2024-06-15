using AutoMapper;
using EntityLayer.Messages;
using EntityLayer.Models.DTOs;
using EntityLayer.Models.Entities;
using EntityLayer.Models.ResponseModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ServiceLayer.Services.AdminService
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public AdminService(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<List<UserDtoForAdmin>>> GetUsersAsync()
        {
            var userList = await _userManager.Users.ToListAsync();
            var mappedUserList = _mapper.Map<List<UserDtoForAdmin>>(userList);

            for (int i = 0; i < userList.Count(); i++)
            {
                var role = await _userManager.GetRolesAsync(userList[i]);
                mappedUserList[i].Role = role.FirstOrDefault()!;
            }

            return CustomResponseDto<List<UserDtoForAdmin>>.Success(mappedUserList, 200);
        }

        public async Task<CustomResponseDto<UserUpdateDtoForAdmin>> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user is null)
            {
                return CustomResponseDto<UserUpdateDtoForAdmin>.Fail(404, CustomErrorMessages.UserNotExist);
            }

            var mappedUser = _mapper.Map<UserUpdateDtoForAdmin>(user);

            return CustomResponseDto<UserUpdateDtoForAdmin>.Success(mappedUser, 200);

        }

        public async Task<CustomResponseDto<NoContentDto>> UserUpdateByAdminAsync(UserUpdateDtoForAdmin request)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            if (user is null)
            {
                return CustomResponseDto<NoContentDto>.Fail(404, CustomErrorMessages.UserNotExist);
            }

            var mappedUser = _mapper.Map(request, user);
            var result = await _userManager.UpdateAsync(mappedUser);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return CustomResponseDto<NoContentDto>.Fail(404, new ErrorDto(errors));
            }

            return CustomResponseDto<NoContentDto>.Success(204);
        }

        public async Task<CustomResponseDto<NoContentDto>> RemoveUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
            {
                return CustomResponseDto<NoContentDto>.Fail(404, new ErrorDto("User not exist"));
            }
            await _userManager.DeleteAsync(user);
            return CustomResponseDto<NoContentDto>.Success(200);
        }


    }
}
