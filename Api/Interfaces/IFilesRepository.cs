using SpeedApply.Api.Models;

namespace SpeedApply.Api.Interfaces
{
    public interface IFilesRepository
    {
        Task<Files?> GetByIdAsync(int id);
    }
}
