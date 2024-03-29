﻿using EntityLayer.Models.DTOs;
using EntityLayer.Models.ResponseModels;
using Microsoft.AspNetCore.Http;

namespace ServiceLayer.Services.AdminService
{
    public interface IAdminService
    {
        Task <CustomResponseDto<List<UserDtoForAdmin>>> GetUsersWithClientIdAsync(HttpContext httpContext);
        Task<CustomResponseDto<UserDtoForAdmin>> GetUserWithClientIdAsync(string userId);
        Task<CustomResponseDto<NoContentDto>> UserUpdateByAdminAsync(UserDtoForAdmin request);
    }
}
