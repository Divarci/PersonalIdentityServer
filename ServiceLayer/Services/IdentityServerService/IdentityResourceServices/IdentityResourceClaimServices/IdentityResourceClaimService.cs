using AutoMapper;
using Duende.IdentityServer.EntityFramework.Entities;
using EntityLayer.Messages;
using EntityLayer.Models.DTOs.IdentityResourceDto.IdentityResourceClaimDto;
using EntityLayer.Models.ResponseModels;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories.IdentityServer;
using RepositoryLayer.UnitOFWorks.IdentityServer;

namespace ServiceLayer.Services.IdentityServerService.IdentityResourceServices.IdentityResourceClaimServices
{
    public class IdentityResourceClaimService : IIdentityResourceClaimService
    {
        private readonly IGenericRepository<IdentityResourceClaim> _identityResourceService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorks _unitOfWorks;

        public IdentityResourceClaimService(IGenericRepository<IdentityResourceClaim> identityResourceService, IMapper mapper, IUnitOfWorks unitOfWorks)
        {
            _identityResourceService = identityResourceService;
            _mapper = mapper;
            _unitOfWorks = unitOfWorks;
        }

        public async Task<CustomResponseDto<List<IdentityResourceClaimItemDto>>> GetAllIdentityResourceClaimsAsync()
        {
            var identityResourceClaims = await _identityResourceService.GetAll().ToListAsync();
            var identityResourceClaimsDto = _mapper.Map<List<IdentityResourceClaimItemDto>>(identityResourceClaims);

            return CustomResponseDto<List<IdentityResourceClaimItemDto>>.Success(identityResourceClaimsDto, 200);
        }

        public async Task<CustomResponseDto<IdentityResourceClaimItemDto>> GetIdentityResourceClaimByIdAsync(int id)
        {
            var identityResourceClaim = await _identityResourceService.GetByIdAsync(id);
            if (identityResourceClaim is null)
            {
                return CustomResponseDto<IdentityResourceClaimItemDto>.Fail(400, new ErrorDto(CustomErrorMessages.ClientNotExist));
            }
            var identityResourceClaimDto = _mapper.Map<IdentityResourceClaimItemDto>(identityResourceClaim);

            return CustomResponseDto<IdentityResourceClaimItemDto>.Success(identityResourceClaimDto, 200);
        }

        public async Task<CustomResponseDto<IdentityResourceClaimCreateDto>> CerateIdentityResourceClaimAsync(IdentityResourceClaimCreateDto request)
        {
            var identityResourceClaim = _mapper.Map<IdentityResourceClaim>(request);
            await _identityResourceService.CreateAsync(identityResourceClaim);
            await _unitOfWorks.SaveChangesAsync();
            return CustomResponseDto<IdentityResourceClaimCreateDto>.Success(request, 201);
        }

        public async Task<CustomResponseDto<NoContentDto>> UpdateIdentityResourceClaimAsync(IdentityResourceClaimUpdateDto request)
        {
            var existIdentityResourceClaim = await _identityResourceService.GetAll().FirstOrDefaultAsync(x => x.Id == request.Id);
            if (existIdentityResourceClaim is null)
            {
                return CustomResponseDto<NoContentDto>.Fail(400, new ErrorDto(CustomErrorMessages.ClientNotExist));
            }
            var identityResourceClaim = _mapper.Map<IdentityResourceClaim>(request);
            _identityResourceService.Update(identityResourceClaim);
            await _unitOfWorks.SaveChangesAsync();
            return CustomResponseDto<NoContentDto>.Success(204);
        }

        public async Task<CustomResponseDto<NoContentDto>> RemoveIdentityResourceClaimAsync(int id)
        {
            var existIdentityResourceClaim = await _identityResourceService.GetAll().FirstOrDefaultAsync(x => x.Id == id);
            if (existIdentityResourceClaim is null)
            {
                return CustomResponseDto<NoContentDto>.Fail(400, new ErrorDto(CustomErrorMessages.ClientNotExist));
            }
            _identityResourceService.Delete(existIdentityResourceClaim);
            await _unitOfWorks.SaveChangesAsync();
            return CustomResponseDto<NoContentDto>.Success(204);
        }
    }
}
