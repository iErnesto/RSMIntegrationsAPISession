using RSMEnterpriseIntegrationsAPI.Application.DTOs;
using RSMEnterpriseIntegrationsAPI.Application.Exceptions;
using RSMEnterpriseIntegrationsAPI.Domain.Interfaces;
using RSMEnterpriseIntegrationsAPI.Domain.Models;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Runtime.Intrinsics.Arm;
using System.Threading.Tasks;

namespace RSMEnterpriseIntegrationsAPI.Application.Services
{
    public class UserLoginService(IUserLoginRepository repository) : IUserLoginService
    {
        private readonly IUserLoginRepository _userLoginRepository = repository;

        public async Task<IEnumerable<GetUserLoginDto>> GetAll()
        {
            var userLogins = await _userLoginRepository.GetUserLogins();

            if (userLogins == null)
            {
                return [];
            }

            List<GetUserLoginDto> userLoginDtos = [];

            foreach (var userLogin in userLogins)
            {
                GetUserLoginDto dto = new GetUserLoginDto
                {
                    Username = userLogin.Username,
                    Role = userLogin.Role,
                };

                userLoginDtos.Add(dto);
            }

            return userLoginDtos;
        }
        public async Task<GetUserLoginDto?> GetUserLoginById(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("UserLoginId is not valid");
            }

            var userLogin = await ValidateUserLoginExistence(id);

            GetUserLoginDto dto = new()
            {
                Username = userLogin.Username,
                Role = userLogin.Role,
            };
            return dto;
        }

        public async Task<int> CreateUserLogin(CreateUserLoginDto createUserLoginDto)
        {
            if (createUserLoginDto is null
            || string.IsNullOrWhiteSpace(createUserLoginDto.Username)
            || string.IsNullOrWhiteSpace(createUserLoginDto.Password)
            || string.IsNullOrWhiteSpace(createUserLoginDto.Role))
            {
                throw new BadRequestException("UserLogin info is not valid");
            }

            UserLogin userLogin = new()
            {
                Username = createUserLoginDto.Username,
                Password = createUserLoginDto.Password,
                Role = createUserLoginDto.Role,
            };
            return await _userLoginRepository.CreateUserLogin(userLogin);

        }

        public async Task<int> DeleteUserLogin(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException($"Unable to delete UserLoginId with id {id}");
            }
            var userLogin = await ValidateUserLoginExistence(id);
            return await _userLoginRepository.DeleteUserLogin(userLogin);
        }

        private async Task<UserLogin> ValidateUserLoginExistence(int id)
        {
            var existingUserLogin = await _userLoginRepository.GetUserLogin(id)
                ?? throw new NotFoundException($"UserLogin with Id: {id} was not found.");

            return existingUserLogin;
        }




    }
}
