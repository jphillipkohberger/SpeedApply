
using SpeedApply.Api.Dtos;
using SpeedApply.Api.Models;


namespace SpeedApply.Api.Interfaces
{
    public interface IUsersService
    {
        Task<UsersDto?> GetUserByIdAsync(int id);
        Task<UsersDto> CreateUserAsync(UsersDto usersDto);
        Task<UsersDto?> LoginUserAsync(UsersDto usersDto);
        Task<List<UsersDto>> GetUsersAsync();
        Task<UsersDto?> GetUserByIdWithQueriesAsync(int id);
        Task<UsersDto?> GetUserByIdWithFilesAsync(int id);
        Task<UsersDto?> SaveUserProfileAsync(int id, string address, string minSal);
    }
}
