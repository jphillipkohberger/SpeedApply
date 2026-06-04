using SpeedApply.Api.Models;

namespace SpeedApply.Api.Interfaces
{
    public interface IUsersRepository
    {
        Task<Users?> GetByIdAsync(int id);
        Task<Users?> GetByEmailAsync(string email);
        Task<List<Users>> GetUsersAsync();
        Task<Users> CreateUserAsync(Users user);
        Task<Users> UpdateUserAsync(Users user);
        Task<Users?> GetUserWithQueriesAsync(int userId);
        Task<Users?> GetUserWithFilesAsync(int userId);
        Task<Users?> GetUserWithQueriesFilesAsync(int userId);
    }
}
