using AutoMapper;
using Duende.IdentityServer.EntityFramework.Entities;
using EntityLayer.Models.DTOs.IdentityResourceDto;

namespace ServiceLayer.Automapper.IdentityServerMapper.IdentotyResourceMappers
{
    public class IdentityResourceMapper : Profile
    {
        public IdentityResourceMapper()
        {
            CreateMap<IdentityResource, IdentityResourceItemDto>().ReverseMap();
            CreateMap<IdentityResource, IdentityResourceCreateDto>().ReverseMap();
            CreateMap<IdentityResource, IdentityResourceUpdateDto>().ReverseMap();
        }
    }
}
