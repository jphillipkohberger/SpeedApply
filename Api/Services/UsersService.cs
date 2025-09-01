using SpeedApply.Api.Dtos;
using SpeedApply.Api.Models;
using SpeedApply.Api.Interfaces;
using System.Collections.Generic;

namespace SpeedApply.Api.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _repository;

        public UsersService(IUsersRepository repository)
        {
            _repository = repository;
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
                Password = usersDto.Password,
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
    }
}
