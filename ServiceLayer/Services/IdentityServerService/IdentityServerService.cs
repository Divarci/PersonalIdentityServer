using AutoMapper;
using Duende.IdentityServer.EntityFramework.Entities;
using Duende.IdentityServer.EntityFramework.Mappers;
using EntityLayer.Messages;
using EntityLayer.Models.DTOs.ClientDto;
using EntityLayer.Models.ResponseModels;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories.IdentityServer;
using RepositoryLayer.UnitOFWorks.IdentityServer;
using Entity = Duende.IdentityServer.EntityFramework.Entities;
using Model = Duende.IdentityServer.Models;

namespace ServiceLayer.Services.IdentityServerService
{
    public class IdentityServerService : IIdentityServerService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Entity.Client> _clientRepository;
        private readonly IUnitOfWorks _unitOfWorks;

        public IdentityServerService(IMapper mapper, IGenericRepository<Client> clientRepository, IUnitOfWorks unitOfWorks)
        {
            _mapper = mapper;
            _clientRepository = clientRepository;
            _unitOfWorks = unitOfWorks;
        }

        #region CLIENT

        public async Task<CustomResponseDto<ClientCreateDto>> CreateClientAsync(ClientCreateDto request)
        {
            var client = _mapper.Map<Model.Client>(request).ToEntity();
            await _clientRepository.CreateAsync(client);
            await _unitOfWorks.SaveChangesAsync();

            return CustomResponseDto<ClientCreateDto>.Success(request, 201);
        }

        public async Task<CustomResponseDto<NoContentDto>> UpdateClientAsync(ClientUpdateDto request)
        {
            var existClient = await _clientRepository.GetAll().Where(x => x.Id == request.Id).Include(x => x.AllowedGrantTypes).Include(x => x.AllowedScopes).Include(x => x.ClientSecrets).FirstOrDefaultAsync();

            if (existClient is null)
            {
                return CustomResponseDto<NoContentDto>.Fail(400, new ErrorDto(CustomErrorMessages.ClientNotExist));
            }

            var client = _mapper.Map<Model.Client>(request).ToEntity();

            var updatedClient = _mapper.Map(client, existClient);
            updatedClient.Id = request.Id;

            _clientRepository.Update(updatedClient);
            await _unitOfWorks.SaveChangesAsync();

            return CustomResponseDto<NoContentDto>.Success(204);
        }

        public async Task<CustomResponseDto<ClientDto>> GetClientByIdAsync(int id)
        {
            var client = await _clientRepository.GetAll().Where(x => x.Id == id).Include(x => x.AllowedGrantTypes).Include(x => x.AllowedScopes).Include(x => x.ClientSecrets).FirstOrDefaultAsync();

            if (client is null)
            {
                return CustomResponseDto<ClientDto>.Fail(400, new ErrorDto(CustomErrorMessages.ClientNotExist));
            }

            var clientDto = _mapper.Map<ClientDto>(client.ToModel());
            clientDto.Id = id;

            return CustomResponseDto<ClientDto>.Success(clientDto, 200);
        }

        public async Task<CustomResponseDto<List<ClientDto>>> GetAllClients()
        {
            var clients = await _clientRepository.GetAll().Include(x => x.AllowedGrantTypes).Include(x => x.AllowedScopes).Include(x => x.ClientSecrets).ToListAsync();

            var clientsModel = new List<Model.Client>();
            foreach (var client in clients)
            {
                clientsModel.Add(client.ToModel());
            }

            var clientsDto = _mapper.Map<List<ClientDto>>(clientsModel);
            return CustomResponseDto<List<ClientDto>>.Success(clientsDto, 200);

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

        #endregion

        #region SCOPES
        #endregion

        #region GRANT-TYPES
        #endregion

        #region SECRETS
        #endregion

    }
}
