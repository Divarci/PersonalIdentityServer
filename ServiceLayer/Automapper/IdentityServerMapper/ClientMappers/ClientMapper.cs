using AutoMapper;
using Duende.IdentityServer.EntityFramework.Entities;
using EntityLayer.Models.DTOs.ClientDto;

namespace ServiceLayer.Automapper.IdentityServerMapper.ClientMappers
{
    public class CLientMapper : Profile
    {
        public CLientMapper()
        {
            CreateMap<Client, ClientCreateDto>().ReverseMap();
            CreateMap<Client, ClientUpdateDto>().ReverseMap();
            CreateMap<Client, ClientItemDto>().ReverseMap();
        }
    }
}
