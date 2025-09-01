
using SpeedApply.Api.Dtos;

namespace SpeedApply.Api.Interfaces
{
    public interface IUsersService
    {
        Task<UsersDto?> GetUserByIdAsync(int id);

        Task<UsersDto> CreateUserAsync(UsersDto usersDto);
        Task<List<UsersDto>> GetUsersAsync();
    }
}
