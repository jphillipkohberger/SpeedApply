using SpeedApply.Api.Models;

namespace SpeedApply.Api.Interfaces
{
    public interface IFilesRepository
    {
        Task<Files?> GetByIdAsync(int id);
        Task<Files> CreateFileAsync(Files file);
    }
}
