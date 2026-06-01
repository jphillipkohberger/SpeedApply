
using SpeedApply.Api.Dtos;

namespace SpeedApply.Api.Interfaces
{
    public interface IFilesService
    {
        Task<FilesDto?> GetFileByIdAsync(int id);
        Task<FilesDto?> SaveUserFileAsync(int id, string name);
    }
}
