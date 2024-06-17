using AutoMapper;
using Duende.IdentityServer.EntityFramework.Entities;
using EntityLayer.Models.DTOs.ApiSideDto.ApiScopeDto;

namespace ServiceLayer.Automapper.IdentityServerMapper.ApiSideMapper.ApiScopeMappers
{
    public class ApiScopeMapper : Profile
    {
        public ApiScopeMapper()
        {
            CreateMap<ApiScope, ApiScopeCreateDto>().ReverseMap();
            CreateMap<ApiScope, ApiScopeUpdateDto>().ReverseMap();
            CreateMap<ApiScope, ApiScopeItemDto>().ReverseMap();
        }
    }
}
