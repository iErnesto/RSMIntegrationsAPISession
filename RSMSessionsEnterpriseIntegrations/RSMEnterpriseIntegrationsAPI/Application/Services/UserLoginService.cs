using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RSMEnterpriseIntegrationsAPI.Application.DTOs;
using RSMEnterpriseIntegrationsAPI.Application.Exceptions;
using RSMEnterpriseIntegrationsAPI.Domain.Interfaces;
using RSMEnterpriseIntegrationsAPI.Domain.Models;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RSMEnterpriseIntegrationsAPI.Application.Services
{
    public class UserLoginService(IUserLoginRepository repository, IPasswordHasher passwordHasher, IOptions<JwtSettings> jwtSettings) : IUserLoginService
    {
        private readonly IUserLoginRepository _userLoginRepository = repository;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;
        private readonly JwtSettings _jwtSettings = jwtSettings.Value;

        public async Task<IEnumerable<GetUserLoginDto>> GetAll()
        {
            var userLogins = await _userLoginRepository.GetUserLogins();

            if (userLogins == null)
            {
                return new List<GetUserLoginDto>();
            }

            var userLoginDtos = new List<GetUserLoginDto>();

            foreach (var userLogin in userLogins)
            {
                var dto = new GetUserLoginDto
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

            var dto = new GetUserLoginDto
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

            var hashedPassword = _passwordHasher.HashPassword(createUserLoginDto.Password);

            var userLogin = new UserLogin
            {
                Username = createUserLoginDto.Username,
                Password = hashedPassword,
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



        private string GenerateJwtToken(int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString())
                ]),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.Expiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

