using SpeedApply.Api.Dtos;
using SpeedApply.Api.Models;
using SpeedApply.Api.Interfaces;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace SpeedApply.Api.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _repository;
        private readonly IPasswordHasher<UsersDto> _passwordHasher;

        public UsersService(IUsersRepository repository, IPasswordHasher<UsersDto> passwordHasher)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
        }

        public async Task<UsersDto?> GetUserByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null) return null;
            
            return new UsersDto { 
                Id = user.Id, 
                UserName = user.UserName, 
                Email = user.Email, 
                Password = user.Password,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task<List<UsersDto>> GetUsersAsync()
        {
            List<Users> users = await _repository.GetUsersAsync();

            List<UsersDto> usersDto = users.Select(u => new UsersDto {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                Password = u.Password,
                CreatedAt = u.CreatedAt
            }).ToList();

            return usersDto;
        }

        
        public async Task<UsersDto> CreateUserAsync(UsersDto usersDto)
        {
            Users user = new Users { 
                UserName = usersDto.UserName,
                Email = usersDto.Email,
                Password = HashPassword(usersDto, usersDto.Password),
                CreatedAt = DateTime.UtcNow
            };

            user = await _repository.CreateUserAsync(user);

            return new UsersDto {
                Id = user.Id,
                UserName = user.UserName, 
                Email = user.Email,
                Password = user.Password,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task<UsersDto?> LoginUserAsync(UsersDto clientUsersDto)
        {
            var user  = await _repository.GetByEmailAsync(clientUsersDto.Email);
           
            if(user == null)
            {
                return null;
            }

            /**
             * clientUsersDto coming from client side
             * dbUsersDto coming from database
             */
            UsersDto dbUsersDto = new UsersDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password,
                CreatedAt = user.CreatedAt
            };

            /**
             * verify client Password vs database PasswordHash
             */
            var passwordResult = _passwordHasher.VerifyHashedPassword(dbUsersDto, dbUsersDto.Password, clientUsersDto.Password);

            if (passwordResult == PasswordVerificationResult.Success)
            {
                // Password is correct, proceed with login
                return dbUsersDto;
            }

            if (passwordResult == PasswordVerificationResult.SuccessRehashNeeded)
            {
                user.Password = HashPassword(dbUsersDto, clientUsersDto.Password);
                user = await _repository.UpdateUserAsync(user);
                return new UsersDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Password = user.Password,
                    CreatedAt = user.CreatedAt
                };
            }

            // password match fail
            return null;
        }

        public String HashPassword(UsersDto usersDto, String password)
        {
            return _passwordHasher.HashPassword(usersDto, password); 
        }
    }
}
