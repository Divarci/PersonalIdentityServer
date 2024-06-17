using AutoMapper;
using Duende.IdentityServer.EntityFramework.Entities;
using EntityLayer.Messages;
using EntityLayer.Models.DTOs.IdentityResourceDto;
using EntityLayer.Models.ResponseModels;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories.IdentityServer;
using RepositoryLayer.UnitOFWorks.IdentityServer;

namespace ServiceLayer.Services.IdentityServerService.IdentityResourceServices
{
    public class IdentityResourceService : IIdentityResourceService
    {
        private readonly IGenericRepository<IdentityResource> _identityResourceService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorks _unitOfWorks;

        public IdentityResourceService(IGenericRepository<IdentityResource> identityResourceService, IMapper mapper, IUnitOfWorks unitOfWorks)
        {
            _identityResourceService = identityResourceService;
            _mapper = mapper;
            _unitOfWorks = unitOfWorks;
        }

        public async Task<CustomResponseDto<IdentityResourceListDto>> GetAllIdentityResourcesAsync()
        {
            var identityResources = await _identityResourceService.GetAll().ToListAsync();
            var identityResourceDto = _mapper.Map<List<IdentityResourceItemDto>>(identityResources);

            var identityResourceListDto = new IdentityResourceListDto { IdentityResources = identityResourceDto };
            identityResourceListDto.HasOpenid = identityResources.Any(x => x.Name == "openid");

            return CustomResponseDto<IdentityResourceListDto>.Success(identityResourceListDto, 200);
        }

        public async Task<CustomResponseDto<IdentityResourceItemDto>> GetIdentityResourceByIdAsync(int id)
        {
            var identityResource = await _identityResourceService.GetByIdAsync(id);
            if (identityResource is null)
            {
                return CustomResponseDto<IdentityResourceItemDto>.Fail(400, new ErrorDto(CustomErrorMessages.ClientNotExist));
            }
            var identityresourceDto = _mapper.Map<IdentityResourceItemDto>(identityResource);

            return CustomResponseDto<IdentityResourceItemDto>.Success(identityresourceDto, 200);
        }

        public async Task<CustomResponseDto<IdentityResourceCreateDto>> CerateIdentityResourceAsync(IdentityResourceCreateDto request)
        {
            var identityResource = _mapper.Map<IdentityResource>(request);
            await _identityResourceService.CreateAsync(identityResource);
            await _unitOfWorks.SaveChangesAsync();
            return CustomResponseDto<IdentityResourceCreateDto>.Success(request, 201);
        }

        public async Task<CustomResponseDto<NoContentDto>> UpdateIdentityResourceAsync(IdentityResourceUpdateDto request)
        {
            var existIdentityResource = await _identityResourceService.GetAll().FirstOrDefaultAsync(x => x.Id == request.Id);
            if (existIdentityResource is null)
            {
                return CustomResponseDto<NoContentDto>.Fail(400, new ErrorDto(CustomErrorMessages.ClientNotExist));
            }
            var identityResource = _mapper.Map<IdentityResource>(request);
            _identityResourceService.Update(identityResource);
            await _unitOfWorks.SaveChangesAsync();
            return CustomResponseDto<NoContentDto>.Success(204);
        }

        public async Task<CustomResponseDto<NoContentDto>> RemoveIdentityResourceAsync(int id)
        {
            var identityResource = await _identityResourceService.GetByIdAsync(id);
            if (identityResource is null)
            {
                return CustomResponseDto<NoContentDto>.Fail(400, new ErrorDto(CustomErrorMessages.ClientNotExist));
            }
            _identityResourceService.Delete(identityResource);
            await _unitOfWorks.SaveChangesAsync();
            return CustomResponseDto<NoContentDto>.Success(204);
        }


    }
}
