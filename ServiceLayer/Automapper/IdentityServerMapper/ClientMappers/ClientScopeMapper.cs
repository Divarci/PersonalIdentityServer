using AutoMapper;
using Duende.IdentityServer.EntityFramework.Entities;
using EntityLayer.Models.DTOs.ClientDto.ClientScopeDto;

namespace ServiceLayer.Automapper.IdentityServerMapper.ClientMappers
{
    public class ClientScopeMapper : Profile
    {
        public ClientScopeMapper()
        {
            CreateMap<ClientScope,ClientScopeCreateDto>().ReverseMap();
            CreateMap<ClientScope,ClientScopeUpdateDto>().ReverseMap();
            CreateMap<ClientScope,ClientScopeItemDto>().ReverseMap();
        }
    }
}
