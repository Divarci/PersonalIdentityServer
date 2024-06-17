using AutoMapper;
using Duende.IdentityServer.EntityFramework.Entities;
using EntityLayer.Models.DTOs.ClientDto.ClientSecretDto;

namespace ServiceLayer.Automapper.IdentityServerMapper.ClientMappers
{
    public class ClientSecretMapper : Profile
    {
        public ClientSecretMapper()
        {
            CreateMap<ClientSecret,ClientSecretCreateDto>().ReverseMap();
            CreateMap<ClientSecret,ClientSecretUpdateDto>().ReverseMap();
            CreateMap<ClientSecret,ClientSecretItemDto>().ReverseMap();
        }
    }
}
