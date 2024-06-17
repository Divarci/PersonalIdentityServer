using AutoMapper;
using Duende.IdentityServer.EntityFramework.Entities;
using EntityLayer.Messages;
using EntityLayer.Models.DTOs.ClientDto.ClientGrantTypeDto;
using EntityLayer.Models.ResponseModels;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories.IdentityServer;
using RepositoryLayer.UnitOFWorks.IdentityServer;

namespace ServiceLayer.Services.IdentityServerService.ClientServices.ClientGrantTypeServices
{
    public class ClientGrantTypeService : IClientGrantTypeService
    {
        private readonly IGenericRepository<ClientGrantType> _grantTypeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorks _unitOfWorks;

        public ClientGrantTypeService(IGenericRepository<ClientGrantType> grantTypeRepository, IMapper mapper, IUnitOfWorks unitOfWorks)
        {
            _grantTypeRepository = grantTypeRepository;
            _mapper = mapper;
            _unitOfWorks = unitOfWorks;
        }

        public async Task<CustomResponseDto<List<ClientGrantTypeItemDto>>> GetAllClientGrantTypesAsync()
        {
            var grantTypes = await _grantTypeRepository.GetAll().ToListAsync();
            var grantTypesDto = _mapper.Map<List<ClientGrantTypeItemDto>>(grantTypes);
            return CustomResponseDto<List<ClientGrantTypeItemDto>>.Success(grantTypesDto, 200);
        }

        public async Task<CustomResponseDto<ClientGrantTypeItemDto>> GetClientGrantTypeByIdAsync(int id)
        {
            var grantType = await _grantTypeRepository.GetByIdAsync(id);
            if (grantType is null)
            {
                return CustomResponseDto<ClientGrantTypeItemDto>.Fail(400, new ErrorDto(CustomErrorMessages.ClientNotExist));
            }
            var grantTypeDto = _mapper.Map<ClientGrantTypeItemDto>(grantType);
            return CustomResponseDto<ClientGrantTypeItemDto>.Success(grantTypeDto, 200);
        }

        public async Task<CustomResponseDto<ClientGrantTypeCreateDto>> CreateClientGrantTypeAsync(ClientGrantTypeCreateDto request)
        {
            var grantType = _mapper.Map<ClientGrantType>(request);
            await _grantTypeRepository.CreateAsync(grantType);
            await _unitOfWorks.SaveChangesAsync();
            return CustomResponseDto<ClientGrantTypeCreateDto>.Success(request, 201);
        }

        public async Task<CustomResponseDto<NoContentDto>> UpdateClientGrantTypeAsync(ClientGrantTypeUpdateDto request)
        {
            var exitGrantType = await _grantTypeRepository.GetAll().FirstOrDefaultAsync(x=>x.Id == request.Id);
            if (exitGrantType is null)
            {
                return CustomResponseDto<NoContentDto>.Fail(400, new ErrorDto(CustomErrorMessages.ClientNotExist));
            }
            var grantType = _mapper.Map<ClientGrantType>(request);
            _grantTypeRepository.Update(grantType);
            await _unitOfWorks.SaveChangesAsync();
            return CustomResponseDto<NoContentDto>.Success(204);
        }

        public async Task<CustomResponseDto<NoContentDto>> RemoveClientGrantTypeAsync(int id)
        {
            var exitGrantType = await _grantTypeRepository.GetByIdAsync(id);
            if (exitGrantType is null)
            {
                return CustomResponseDto<NoContentDto>.Fail(400, new ErrorDto(CustomErrorMessages.ClientNotExist));
            }
            _grantTypeRepository.Delete(exitGrantType);
            await _unitOfWorks.SaveChangesAsync();
            return CustomResponseDto<NoContentDto>.Success(204);
        }
    }
}
