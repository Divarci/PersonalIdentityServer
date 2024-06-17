using AutoMapper;
using Duende.IdentityServer.EntityFramework.Entities;
using EntityLayer.Messages;
using EntityLayer.Models.DTOs.ClientDto.ClientScopeDto;
using EntityLayer.Models.DTOs.ClientDto.ClientSecretDto;
using EntityLayer.Models.ResponseModels;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories.IdentityServer;
using RepositoryLayer.UnitOFWorks.IdentityServer;

namespace ServiceLayer.Services.IdentityServerService.ClientServices.ClientSecretServices
{
    public class ClientSecretService : IClientSecretService
    {
        private readonly IGenericRepository<ClientSecret> _secretRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorks _unitOfWorks;

        public ClientSecretService(IGenericRepository<ClientSecret> grantTypeRepository, IMapper mapper, IUnitOfWorks unitOfWorks)
        {
            _secretRepository = grantTypeRepository;
            _mapper = mapper;
            _unitOfWorks = unitOfWorks;
        }

        public async Task<CustomResponseDto<List<ClientSecretItemDto>>> GetAllClientSecretsAsync()
        {
            var secrets = await _secretRepository.GetAll().ToListAsync();
            var secretsDto = _mapper.Map<List<ClientSecretItemDto>>(secrets);
            return CustomResponseDto<List<ClientSecretItemDto>>.Success(secretsDto, 200);
        }
        public async Task<CustomResponseDto<ClientSecretItemDto>> GetClientSecretByIdAsync(int id)
        {
            var secret = await _secretRepository.GetByIdAsync(id);
            if (secret is null)
            {
                return CustomResponseDto<ClientSecretItemDto>.Fail(400, new ErrorDto(CustomErrorMessages.ClientNotExist));
            }
            var secretDto = _mapper.Map<ClientSecretItemDto>(secret);
            return CustomResponseDto<ClientSecretItemDto>.Success(secretDto, 200);
        }
        public async Task<CustomResponseDto<ClientSecretCreateDto>> CreateClientSecretAsync(ClientSecretCreateDto request)
        {
            var secret = _mapper.Map<ClientSecret>(request);
            await _secretRepository.CreateAsync(secret);
            await _unitOfWorks.SaveChangesAsync();
            return CustomResponseDto<ClientSecretCreateDto>.Success(request, 201);
        }
        public async Task<CustomResponseDto<NoContentDto>> UpdateClientSecretAsync(ClientSecretUpdateDto request)
        {
            var existSecret = await _secretRepository.GetAll().FirstOrDefaultAsync(x=>x.Id == request.Id);
            if (existSecret is null)
            {
                return CustomResponseDto<NoContentDto>.Fail(400, new ErrorDto(CustomErrorMessages.ClientNotExist));
            }
            var secret = _mapper.Map<ClientSecret>(request);
            _secretRepository.Update(secret);
            await _unitOfWorks.SaveChangesAsync();
            return CustomResponseDto<NoContentDto>.Success(204);
        }
        public async Task<CustomResponseDto<NoContentDto>> RemoveClientSecretAsync(int id)
        {
            var existSecret = await _secretRepository.GetByIdAsync(id);
            if (existSecret is null)
            {
                return CustomResponseDto<NoContentDto>.Fail(400, new ErrorDto(CustomErrorMessages.ClientNotExist));
            }
            _secretRepository.Delete(existSecret);
            await _unitOfWorks.SaveChangesAsync();
            return CustomResponseDto<NoContentDto>.Success(204);
        }
    }
}
