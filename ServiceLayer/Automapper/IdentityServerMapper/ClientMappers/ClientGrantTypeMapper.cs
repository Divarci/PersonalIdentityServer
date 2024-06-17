using AutoMapper;
using Duende.IdentityServer.EntityFramework.Entities;
using EntityLayer.Models.DTOs.ClientDto.ClientGrantTypeDto;

namespace ServiceLayer.Automapper.IdentityServerMapper.ClientMappers
{
    public class ClientGrantTypeMapper : Profile
    {
        public ClientGrantTypeMapper()
        {
            CreateMap<ClientGrantType,ClientGrantTypeCreateDto>().ReverseMap();
            CreateMap<ClientGrantType,ClientGrantTypeUpdateDto>().ReverseMap();
            CreateMap<ClientGrantType,ClientGrantTypeItemDto>().ReverseMap();
        }
    }
}
