using AutoMapper;
using Duende.IdentityServer.EntityFramework.Entities;
using EntityLayer.Messages;
using EntityLayer.Models.DTOs.ClientDto;
using EntityLayer.Models.ResponseModels;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories.IdentityServer;
using RepositoryLayer.UnitOFWorks.IdentityServer;

namespace ServiceLayer.Services.IdentityServerService.ClientServices
{
    public class ClientService : IClientService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Client> _clientRepository;
        private readonly IUnitOfWorks _unitOfWorks;

        public ClientService(IMapper mapper, IGenericRepository<Client> clientRepository, IUnitOfWorks unitOfWorks)
        {
            _mapper = mapper;
            _clientRepository = clientRepository;
            _unitOfWorks = unitOfWorks;
        }

        public async Task<CustomResponseDto<ClientCreateDto>> CreateClientAsync(ClientCreateDto request)
        {
            var client = _mapper.Map<Client>(request);
            await _clientRepository.CreateAsync(client);
            await _unitOfWorks.SaveChangesAsync();

            return CustomResponseDto<ClientCreateDto>.Success(request, 201);
        }

        public async Task<CustomResponseDto<NoContentDto>> UpdateClientAsync(ClientUpdateDto request)
        {
            var existClient = await _clientRepository.GetAll().FirstOrDefaultAsync(x => x.Id == request.Id);
            if (existClient is null)
            {
                return CustomResponseDto<NoContentDto>.Fail(400, new ErrorDto(CustomErrorMessages.ClientNotExist));
            }

            var client = _mapper.Map<Client>(request);
            _clientRepository.Update(client);
            await _unitOfWorks.SaveChangesAsync();

            return CustomResponseDto<NoContentDto>.Success(204);
        }

        public async Task<CustomResponseDto<ClientItemDto>> GetClientByIdAsync(int id)
        {
            var client = await _clientRepository.GetAll().Include(x => x.AllowedGrantTypes).Include(x => x.ClientSecrets).Include(x => x.AllowedScopes).FirstOrDefaultAsync(x => x.Id == id);
            if (client is null)
            {
                return CustomResponseDto<ClientItemDto>.Fail(400, new ErrorDto(CustomErrorMessages.ClientNotExist));
            }

            var clientDto = _mapper.Map<ClientItemDto>(client);

            clientDto.HasClientGrantType = clientDto.AllowedGrantTypes.Any();
            clientDto.HasClientScope = clientDto.AllowedScopes.Any();
            clientDto.HasClientSecret = clientDto.ClientSecrets.Any();

            return CustomResponseDto<ClientItemDto>.Success(clientDto, 200);
        }

        public async Task<CustomResponseDto<List<ClientItemDto>>> GetAllClients()
        {
            var clients = await _clientRepository.GetAll().Include(x => x.AllowedGrantTypes).Include(x => x.ClientSecrets).Include(x => x.AllowedScopes).ToListAsync();

            var clientsDto = _mapper.Map<List<ClientItemDto>>(clients);
            foreach (var clientDto in clientsDto)
            {
                clientDto.HasClientGrantType = clientDto.AllowedGrantTypes.Any();
                clientDto.HasClientScope = clientDto.AllowedScopes.Any();
                clientDto.HasClientSecret = clientDto.ClientSecrets.Any();
            }
            return CustomResponseDto<List<ClientItemDto>>.Success(clientsDto, 200);

        }

        public async Task<CustomResponseDto<NoContentDto>> RemoveClientAsync(int id)
        {
            var existingClient = await _clientRepository.GetByIdAsync(id);
            if (existingClient is null)
            {
                return CustomResponseDto<NoContentDto>.Fail(400, new ErrorDto(CustomErrorMessages.ClientNotExist));
            }

            _clientRepository.Delete(existingClient);
            await _unitOfWorks.SaveChangesAsync();
            return CustomResponseDto<NoContentDto>.Success(204);
        }



    }
}
