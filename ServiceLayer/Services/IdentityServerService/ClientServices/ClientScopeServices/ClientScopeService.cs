using AutoMapper;
using Duende.IdentityServer.EntityFramework.Entities;
using EntityLayer.Messages;
using EntityLayer.Models.DTOs.ClientDto.ClientGrantTypeDto;
using EntityLayer.Models.DTOs.ClientDto.ClientScopeDto;
using EntityLayer.Models.ResponseModels;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories.IdentityServer;
using RepositoryLayer.UnitOFWorks.IdentityServer;

namespace ServiceLayer.Services.IdentityServerService.ClientServices.ClientScopeServices
{
    public class ClientScopeService : IClientScopeService
    {
        private readonly IGenericRepository<ClientScope> _scopeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorks _unitOfWorks;

        public ClientScopeService(IGenericRepository<ClientScope> grantTypeRepository, IMapper mapper, IUnitOfWorks unitOfWorks)
        {
            _scopeRepository = grantTypeRepository;
            _mapper = mapper;
            _unitOfWorks = unitOfWorks;
        }

        public async Task<CustomResponseDto<List<ClientScopeItemDto>>> GetAllClientScopesAsync()
        {
            var scopes = await _scopeRepository.GetAll().ToListAsync();
            var scopesDto = _mapper.Map<List<ClientScopeItemDto>>(scopes);
            return CustomResponseDto<List<ClientScopeItemDto>>.Success(scopesDto, 200);
        }
        public async Task<CustomResponseDto<ClientScopeItemDto>> GetClientScopeByIdAsync(int id)
        {
            var scope = await _scopeRepository.GetByIdAsync(id);
            if (scope is null)
            {
                return CustomResponseDto<ClientScopeItemDto>.Fail(400, new ErrorDto(CustomErrorMessages.ClientNotExist));
            }
            var scopeDto = _mapper.Map<ClientScopeItemDto>(scope);
            return CustomResponseDto<ClientScopeItemDto>.Success(scopeDto, 200);
        }

        public async Task<CustomResponseDto<ClientScopeCreateDto>> CreateClientScopeAsync(ClientScopeCreateDto request)
        {
            var scope = _mapper.Map<ClientScope>(request);
            await _scopeRepository.CreateAsync(scope);
            await _unitOfWorks.SaveChangesAsync();
            return CustomResponseDto<ClientScopeCreateDto>.Success(request, 201);
        }

        public async Task<CustomResponseDto<NoContentDto>> UpdateClientScopeAsync(ClientScopeUpdateDto request)
        {
            var exiatScope = await _scopeRepository.GetAll().FirstOrDefaultAsync(x=>x.Id == request.Id);
            if (exiatScope is null)
            {
                return CustomResponseDto<NoContentDto>.Fail(400, new ErrorDto(CustomErrorMessages.ClientNotExist));
            }
            var scope = _mapper.Map<ClientScope>(request);
            _scopeRepository.Update(scope);
            await _unitOfWorks.SaveChangesAsync();
            return CustomResponseDto<NoContentDto>.Success(204);
        }

        public async Task<CustomResponseDto<NoContentDto>> RemoveClientScopeAsync(int id)
        {
            var scope = await _scopeRepository.GetByIdAsync(id);
            if (scope is null)
            {
                return CustomResponseDto<NoContentDto>.Fail(400, new ErrorDto(CustomErrorMessages.ClientNotExist));
            }
            _scopeRepository.Delete(scope);
            await _unitOfWorks.SaveChangesAsync();
            return CustomResponseDto<NoContentDto>.Success(204);
        }
    }
}
