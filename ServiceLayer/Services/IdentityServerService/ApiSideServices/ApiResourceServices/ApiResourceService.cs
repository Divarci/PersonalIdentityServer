using AutoMapper;
using Duende.IdentityServer.EntityFramework.Entities;
using EntityLayer.Messages;
using EntityLayer.Models.DTOs.ApiSideDto.ApiResourceDto;
using EntityLayer.Models.ResponseModels;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories.IdentityServer;
using RepositoryLayer.UnitOFWorks.IdentityServer;

namespace ServiceLayer.Services.IdentityServerService.ApiSideServices.ApiResourceServices
{
    public class ApiResourceService : IApiResourceService
    {
        private readonly IGenericRepository<ApiResource> _resourceRepository;
        private readonly IGenericRepository<ApiScope> _scopeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorks _unitOfWorks;

        public ApiResourceService(IGenericRepository<ApiResource> resourceRepository, IMapper mapper, IUnitOfWorks unitOfWorks, IGenericRepository<ApiScope> scopeRepository)
        {
            _resourceRepository = resourceRepository;
            _mapper = mapper;
            _unitOfWorks = unitOfWorks;
            _scopeRepository = scopeRepository;
        }

        public async Task<CustomResponseDto<List<ApiResourceItemDto>>> GetAllApiResourcesAsync()
        {
            var resources = await _resourceRepository.GetAll().Include(x => x.Scopes).ToListAsync();
            var apiScopes = await _scopeRepository.GetAll().ToListAsync();
            
            var resourceDto = _mapper.Map<List<ApiResourceItemDto>>(resources);

            for (int i = 0; i < resources.Count; i++)
            {
                resourceDto[i].IsScopeAssigned = resources[i].Scopes.Any();

                if (resourceDto[i].IsScopeAssigned)
                {
                    var count = 0;
                    foreach (var scope in resourceDto[i].Scopes)
                    {
                        count = apiScopes.Any(x => x.Name == scope.Scope) ? count+1 : count;
                    }

                    resourceDto[i].IsScopesHavePerfectMatch = resourceDto[i].Scopes.Count == count;
                }
            }

            return CustomResponseDto<List<ApiResourceItemDto>>.Success(resourceDto, 200);
        }

        public async Task<CustomResponseDto<ApiResourceItemDto>> GetApiResourceByIdAsync(int id)
        {
            var existResource = await _resourceRepository.GetAll().Include(x => x.Scopes).FirstOrDefaultAsync(x => x.Id == id);
            var apiScopes = await _scopeRepository.GetAll().ToListAsync();

            if (existResource is null)
            {
                return CustomResponseDto<ApiResourceItemDto>.Fail(400, new ErrorDto(CustomErrorMessages.ScopeNotExist));
            }

            var recourceDto = _mapper.Map<ApiResourceItemDto>(existResource);
            recourceDto.IsScopeAssigned = existResource.Scopes.Any();
            if (recourceDto.IsScopeAssigned)
            {
                var count = 0;
                foreach (var scope in recourceDto.Scopes)
                {
                    count = apiScopes.Any(x => x.Name == scope.Scope) ? count + 1 : count;
                }

                recourceDto.IsScopesHavePerfectMatch = recourceDto.Scopes.Count == count;
            }

            return CustomResponseDto<ApiResourceItemDto>.Success(recourceDto, 200);
        }

        public async Task<CustomResponseDto<ApiResourceCreateDto>> AddApiResourceAsync(ApiResourceCreateDto request)
        {
            var resource = _mapper.Map<ApiResource>(request);
            await _resourceRepository.CreateAsync(resource);
            await _unitOfWorks.SaveChangesAsync();
            return CustomResponseDto<ApiResourceCreateDto>.Success(request, 201);
        }

        public async Task<CustomResponseDto<NoContentDto>> UpdateApiResourceAsync(ApiResourceUpdateDto request)
        {
            var existResource = await _resourceRepository.GetAll().Include(x => x.Scopes).FirstOrDefaultAsync(x => x.Id == request.Id);
            if (existResource is null)
            {
                return CustomResponseDto<NoContentDto>.Fail(400, new ErrorDto(CustomErrorMessages.ScopeNotExist));
            }

            var resource = _mapper.Map<ApiResource>(request);
            resource.Id = request.Id;
            _resourceRepository.Update(resource);
            await _unitOfWorks.SaveChangesAsync();
            return CustomResponseDto<NoContentDto>.Success(204);
        }

        public async Task<CustomResponseDto<NoContentDto>> RemoveApiResourceAsync(int id)
        {
            var existResource = await _resourceRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
            if (existResource is null)
            {
                return CustomResponseDto<NoContentDto>.Fail(400, new ErrorDto(CustomErrorMessages.ScopeNotExist));
            }
            _resourceRepository.Delete(existResource);
            await _unitOfWorks.SaveChangesAsync();
            return CustomResponseDto<NoContentDto>.Success(204);
        }
    }
}
