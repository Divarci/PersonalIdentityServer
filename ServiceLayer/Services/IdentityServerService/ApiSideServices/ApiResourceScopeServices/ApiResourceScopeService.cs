using AutoMapper;
using Azure.Core;
using EntityLayer.Messages;
using EntityLayer.Models.DTOs.ApiSideDto.ApiResourceDto.ApiResourceScopeDto;
using EntityLayer.Models.ResponseModels;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories.IdentityServer;
using RepositoryLayer.UnitOFWorks.IdentityServer;
using Entity = Duende.IdentityServer.EntityFramework.Entities;

namespace ServiceLayer.Services.IdentityServerService.ApiSideServices.ApiResourceScopeServices
{
    public class ApiResourceScopeService : IApiResourceScopeService
    {
        private readonly IGenericRepository<Entity.ApiResourceScope> _resourceScopeRepository;
        private readonly IGenericRepository<Entity.ApiResource> _resourceRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorks _unitOfWorks;

        public ApiResourceScopeService(IGenericRepository<Entity.ApiResourceScope> resourceScopeRepository, IMapper mapper, IUnitOfWorks unitOfWorks, IGenericRepository<Entity.ApiResource> resourceRepository)
        {
            _resourceScopeRepository = resourceScopeRepository;
            _mapper = mapper;
            _unitOfWorks = unitOfWorks;
            _resourceRepository = resourceRepository;
        }

        public async Task<CustomResponseDto<List<ApiResourceScopeItemDto>>> GetAllApiResourceScopeListAsync()
        {
            var resourceScopes = await _resourceScopeRepository.GetAll().ToListAsync();
            var resourceScopesDto = _mapper.Map<List<ApiResourceScopeItemDto>>(resourceScopes);
            return CustomResponseDto<List<ApiResourceScopeItemDto>>.Success(resourceScopesDto, 200);
        }

        public async Task<CustomResponseDto<ApiResourceScopeItemDto>> GetApiResourceScopeByIdAsync(int id)
        {
            var existResourceScope = await _resourceScopeRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
            if (existResourceScope is null)
            {
                return CustomResponseDto<ApiResourceScopeItemDto>.Fail(400, new ErrorDto(CustomErrorMessages.ScopeNotExist));
            }
            var resourceScope = await _resourceScopeRepository.GetByIdAsync(id);
            var resourceScopeDto = _mapper.Map<ApiResourceScopeItemDto>(resourceScope);
            return CustomResponseDto<ApiResourceScopeItemDto>.Success(resourceScopeDto, 200);
        }

        public async Task<CustomResponseDto<ApiResourceScopeCreateDto>> CreateApiResourceScopeAsync(ApiResourceScopeCreateDto request)
        {
            var resourceScope = _mapper.Map<Entity.ApiResourceScope>(request);
            await _resourceScopeRepository.CreateAsync(resourceScope);
            await _unitOfWorks.SaveChangesAsync();
            return CustomResponseDto<ApiResourceScopeCreateDto>.Success(request, 201);
        }

        public async Task<CustomResponseDto<NoContentDto>> UpdateApiResourceScopeAsync(ApiResourceScopeUpdateDto request)
        {
            var existResourceScope = await _resourceScopeRepository.GetAll().FirstOrDefaultAsync(x => x.Id == request.Id);
            if(existResourceScope is null)
            {
                return CustomResponseDto<NoContentDto>.Fail(400, new ErrorDto(CustomErrorMessages.ScopeNotExist));
            }

            var resourceScope = _mapper.Map<Entity.ApiResourceScope>(request);
            _resourceScopeRepository.Update(resourceScope);
            await _unitOfWorks.SaveChangesAsync();
            return CustomResponseDto<NoContentDto>.Success(204);
        }

        public async Task<CustomResponseDto<NoContentDto>> RemoveApiResourceScopeAsync(int id)
        {
            var existResourceScope = await _resourceScopeRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
            if (existResourceScope is null)
            {
                return CustomResponseDto<NoContentDto>.Fail(400, new ErrorDto(CustomErrorMessages.ScopeNotExist));
            }

            _resourceScopeRepository.Delete(existResourceScope);
            await _unitOfWorks.SaveChangesAsync();
            return CustomResponseDto<NoContentDto>.Success(204);
        }
    }
}
