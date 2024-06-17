using AutoMapper;
using EntityLayer.Models.DTOs.AuthenticationDto;
using EntityLayer.Models.Entities;

namespace ServiceLayer.Automapper
{
    public class AppUserMapper : Profile
    {
        public AppUserMapper()
        {
            CreateMap<AppUser,RegisterDto>().ReverseMap();
            CreateMap<AppUser,UserDtoForAdmin>().ReverseMap();
            CreateMap<AppUser,UserDtoForMember>().ReverseMap();
            CreateMap<AppUser,PasswordUpdateDto>().ReverseMap();
            CreateMap<AppUser,UserUpdateDtoForAdmin>().ReverseMap();
        }
    }
}
