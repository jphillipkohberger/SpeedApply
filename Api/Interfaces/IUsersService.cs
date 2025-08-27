
using SpeedApply.Api.Dtos;

namespace SpeedApply.Api.Interfaces
{
    public interface IUsersService
    {
        Task<UsersDto> GetUserByIdAsync(int id);
        Task<List<UsersDto>> GetUsersAsync();
    }
}
