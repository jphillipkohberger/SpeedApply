using SpeedApply.Api.Models;
using SpeedApply.Api.Dtos;

namespace SpeedApply.Api.Interfaces
{
    public interface IUsersRepository
    {
        Task<Users> GetByIdAsync(int id);

        Task<List<Users>> GetUsersAsync();
    }
}
