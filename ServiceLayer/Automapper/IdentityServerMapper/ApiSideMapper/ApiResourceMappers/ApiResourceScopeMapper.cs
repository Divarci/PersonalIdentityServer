using AutoMapper;
using Duende.IdentityServer.EntityFramework.Entities;
using EntityLayer.Models.DTOs.ApiSideDto.ApiResourceDto.ApiResourceScopeDto;

namespace ServiceLayer.Automapper.IdentityServerMapper.ApiSideMapper.ApiResourceMappers
{
    public class ApiResourceScopeMapper : Profile
    {
        public ApiResourceScopeMapper()
        {
            CreateMap<ApiResourceScope, ApiResourceScopeCreateDto>().ReverseMap();
            CreateMap<ApiResourceScope, ApiResourceScopeUpdateDto>().ReverseMap();
            CreateMap<ApiResourceScope, ApiResourceScopeItemDto>().ReverseMap();
        }
    }
}
