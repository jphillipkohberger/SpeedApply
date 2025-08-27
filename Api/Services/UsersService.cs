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

        public async Task<UsersDto> GetUserByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null) return null;
            
            return new UsersDto { Id = user.Id, UserName = user.UserName };
        }

        public async Task<List<UsersDto>> GetUsersAsync()
        {
            List<Users> users = await _repository.GetUsersAsync();

            List<UsersDto> usersDto = users.Select(u => new UsersDto
                {
                    Id = u.Id,
                    UserName = u.UserName
                }
            ).ToList();

            return usersDto;
        }

        
        public async Task<UsersDto> CreateUserAsync()
        {
            Random random = new Random();
            int randomNumber = random.Next(); // Generates a non-negative random integer
            string randomNumberString = randomNumber.ToString();

            Users user = new Users { 
                UserName = "testuser0" + randomNumberString, 
                Email = randomNumberString + "test@example.co", 
                Password = "password0" 
            };

            user = await _repository.CreateUserAsync(user);

            return new UsersDto { Id = user.Id, UserName = user.UserName, Email = user.Email };
        }
    }
}
