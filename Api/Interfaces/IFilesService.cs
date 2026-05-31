
using SpeedApply.Api.Dtos;

namespace SpeedApply.Api.Interfaces
{
    public interface IFilesService
    {
        Task<FilesDto?> GetFileByIdAsync(int id);
    }
}
