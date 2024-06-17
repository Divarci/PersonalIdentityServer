using AutoMapper;
using Duende.IdentityServer.EntityFramework.Entities;
using EntityLayer.Models.DTOs.IdentityResourceDto.IdentityResourceClaimDto;

namespace ServiceLayer.Automapper.IdentityServerMapper.IdentotyResourceMappers
{
    public class IdentityResourceClaimMapper : Profile
    {
        public IdentityResourceClaimMapper()
        {
            CreateMap<IdentityResourceClaim,IdentityResourceClaimItemDto>().ReverseMap();
            CreateMap<IdentityResourceClaim,IdentityResourceClaimCreateDto>().ReverseMap();
            CreateMap<IdentityResourceClaim,IdentityResourceClaimUpdateDto>().ReverseMap();
        }
    }
}
