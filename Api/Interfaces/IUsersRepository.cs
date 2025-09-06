using SpeedApply.Api.Models;
using SpeedApply.Api.Dtos;

namespace SpeedApply.Api.Interfaces
{
    public interface IUsersRepository
    {
        Task<Users?> GetByIdAsync(int id);
        Task<Users?> GetByEmailAsync(string email);
        Task<List<Users>> GetUsersAsync();
        Task<Users> CreateUserAsync(Users user);
        Task<Users> UpdateUserAsync(Users user);
    }
}
