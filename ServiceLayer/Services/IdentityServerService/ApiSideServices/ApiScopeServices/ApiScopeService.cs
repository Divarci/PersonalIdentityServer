using AutoMapper;
using Duende.IdentityServer.EntityFramework.Entities;
using EntityLayer.Messages;
using EntityLayer.Models.DTOs.ApiSideDto.ApiScopeDto;
using EntityLayer.Models.ResponseModels;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories.IdentityServer;
using RepositoryLayer.UnitOFWorks.IdentityServer;

namespace ServiceLayer.Services.IdentityServerService.ApiSideServices.ApiScopeServices
{
    public class ApiScopeService : IApiScopeService
    {
        private readonly IGenericRepository<ApiScope> _scopeRepository;
        private readonly IGenericRepository<ApiResource> _resourceRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorks _unitOfWorks;

        public ApiScopeService(IGenericRepository<ApiScope> scopeRepository, IMapper mapper, IUnitOfWorks unitOfWorks, IGenericRepository<ApiResource> resourceRepository)
        {
            _scopeRepository = scopeRepository;
            _mapper = mapper;
            _unitOfWorks = unitOfWorks;
            _resourceRepository = resourceRepository;
        }

        public async Task<CustomResponseDto<List<ApiScopeItemDto>>> GetAllApiScopesAsync()
        {
            var scopes = await _scopeRepository.GetAll().ToListAsync();
            var resources = await _resourceRepository.GetAll().Include(x=>x.Scopes).ToListAsync();

            var scopeDto = new List<ApiScopeItemDto>();
           
            for (int i = 0; i < scopes.Count; i++)
            {
                scopeDto.Add(_mapper.Map<ApiScopeItemDto>(scopes[i]));
                scopeDto[i].Id = scopes[i].Id;
                scopeDto[i].IsAssignedToResource = resources.Any(resource => resource.Scopes.Any(x => x.Scope == scopes[i].Name));
            }         
            
            return CustomResponseDto<List<ApiScopeItemDto>>.Success(scopeDto, 200);
        }

        public async Task<CustomResponseDto<ApiScopeItemDto>> GetApiScopeByIdAsync(int id)
        {
            var existScope = await _scopeRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
            if (existScope is null)
            {
                return CustomResponseDto<ApiScopeItemDto>.Fail(400, new ErrorDto(CustomErrorMessages.ScopeNotExist));
            }
            var scopeDto = _mapper.Map<ApiScopeItemDto>(existScope);
            return CustomResponseDto<ApiScopeItemDto>.Success(scopeDto, 200);
        }

        public async Task<CustomResponseDto<ApiScopeCreateDto>> AddApiScopeAsync(ApiScopeCreateDto request)
        {
            var scope = _mapper.Map<ApiScope>(request);
            await _scopeRepository.CreateAsync(scope);
            await _unitOfWorks.SaveChangesAsync();
            return CustomResponseDto<ApiScopeCreateDto>.Success(request, 201);
        }

        public async Task<CustomResponseDto<NoContentDto>> UpdateApiScopeAsync(ApiScopeUpdateDto request)
        {
            var existScope = await _scopeRepository.GetAll().FirstOrDefaultAsync(x => x.Id == request.Id);
            if (existScope is null)
            {
                return CustomResponseDto<NoContentDto>.Fail(400, new ErrorDto(CustomErrorMessages.ScopeNotExist));
            }
            var scope = _mapper.Map<ApiScope>(request);
            scope.Id = request.Id;
            _scopeRepository.Update(scope);
            await _unitOfWorks.SaveChangesAsync();
            return CustomResponseDto<NoContentDto>.Success(204);
        }

        public async Task<CustomResponseDto<NoContentDto>> RemoveApiScopeAsync(int id)
        {
            var existScope = await _scopeRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
            if (existScope is null)
            {
                return CustomResponseDto<NoContentDto>.Fail(400, new ErrorDto(CustomErrorMessages.ScopeNotExist));
            }
            _scopeRepository.Delete(existScope);
            await _unitOfWorks.SaveChangesAsync();
            return CustomResponseDto<NoContentDto>.Success(204);
        }
    }
}
