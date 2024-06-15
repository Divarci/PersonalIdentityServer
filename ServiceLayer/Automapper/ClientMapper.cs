using AutoMapper;
using Duende.IdentityServer.Models;
using EntityLayer.Models.DTOs.ClientDto;

namespace ServiceLayer.Automapper
{
    public class CLientMapper : Profile
    {
        public CLientMapper()
        {
            CreateMap<Client, ClientCreateDto>().ReverseMap();
            CreateMap<Client, ClientUpdateDto>().ReverseMap();
            CreateMap<Client, ClientDto>().ReverseMap();
        }
    }
}
