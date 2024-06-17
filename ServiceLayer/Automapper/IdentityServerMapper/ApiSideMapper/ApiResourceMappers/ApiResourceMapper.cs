using AutoMapper;
using Duende.IdentityServer.EntityFramework.Entities;
using EntityLayer.Models.DTOs.ApiSideDto.ApiResourceDto;

namespace ServiceLayer.Automapper.IdentityServerMapper.ApiSideMapper.ApiResourceMappers
{
    public class ApiResourceMapper : Profile
    {
        public ApiResourceMapper()
        {
            CreateMap<ApiResource, ApiResourceCreateDto>().ReverseMap();
            CreateMap<ApiResource, ApiResourceUpdateDto>().ReverseMap();
            CreateMap<ApiResource, ApiResourceItemDto>().ReverseMap();
        }
    }
}
