using AutoMapper;
using Duende.IdentityServer.EntityFramework.Mappers;
using EntityLayer.Messages;
using EntityLayer.Models.DTOs;
using EntityLayer.Models.DTOs.ClientDto;
using EntityLayer.Models.Entities;
using EntityLayer.Models.ResponseModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories.IdentityServer;
using RepositoryLayer.UnitOFWorks.IdentityServer;
using Entity = Duende.IdentityServer.EntityFramework.Entities;
using Model = Duende.IdentityServer.Models;

namespace ServiceLayer.Services.AdminService
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Entity.Client> _clientRepository;
        private readonly IUnitOfWorks _unitOfWorks;

        public AdminService(UserManager<AppUser> userManager, IMapper mapper, IGenericRepository<Entity.Client> clientRepository, IUnitOfWorks unitOfWorks)
        {
            _userManager = userManager;
            _mapper = mapper;
            _clientRepository = clientRepository;
            _unitOfWorks = unitOfWorks;
        }


        #region USER
        public async Task<CustomResponseDto<List<UserDtoForAdmin>>> GetUsersAsync()
        {
            var userList = await _userManager.Users.ToListAsync();
            var mappedUserList = _mapper.Map<List<UserDtoForAdmin>>(userList);

            for (int i = 0; i < userList.Count(); i++)
            {
                var role = await _userManager.GetRolesAsync(userList[i]);
                mappedUserList[i].Role = role.FirstOrDefault()!;
            }

            return CustomResponseDto<List<UserDtoForAdmin>>.Success(mappedUserList, 200);
        }

        public async Task<CustomResponseDto<UserUpdateDtoForAdmin>> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user is null)
            {
                return CustomResponseDto<UserUpdateDtoForAdmin>.Fail(404, CustomErrorMessages.UserNotExist);
            }

            var mappedUser = _mapper.Map<UserUpdateDtoForAdmin>(user);

            return CustomResponseDto<UserUpdateDtoForAdmin>.Success(mappedUser, 200);

        }

        public async Task<CustomResponseDto<NoContentDto>> UserUpdateByAdminAsync(UserUpdateDtoForAdmin request)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            if (user is null)
            {
                return CustomResponseDto<NoContentDto>.Fail(404, CustomErrorMessages.UserNotExist);
            }

            var mappedUser = _mapper.Map(request, user);
            var result = await _userManager.UpdateAsync(mappedUser);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return CustomResponseDto<NoContentDto>.Fail(404, new ErrorDto(errors));
            }

            return CustomResponseDto<NoContentDto>.Success(204);
        }

        public async Task<CustomResponseDto<NoContentDto>> RemoveUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
            {
                return CustomResponseDto<NoContentDto>.Fail(404, new ErrorDto("User not exist"));
            }
            await _userManager.DeleteAsync(user);
            return CustomResponseDto<NoContentDto>.Success(200);
        }

        #endregion

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

    }
}
