
using SpeedApply.Api.Dtos;

namespace SpeedApply.Api.Interfaces
{
    public interface IRootUrlsService
    {
        Task<List<RootUrlsDto>> GetRootUrlsAsync();
        Task<RootUrlsDto?> GetRootUrlByIdAsync(int id);
    }
}
