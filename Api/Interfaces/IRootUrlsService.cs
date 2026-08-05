
using SpeedApply.Api.Dtos;

namespace SpeedApply.Api.Interfaces
{
    public interface IRootUrlsService
    {
        Task<List<RootUrlsDto>> GetRootUrlsAsync(string query);
        Task<RootUrlsDto?> GetRootUrlByIdAsync(int id);
    }
}
