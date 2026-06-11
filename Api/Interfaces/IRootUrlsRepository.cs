using SpeedApply.Api.Models;

namespace SpeedApply.Api.Interfaces
{
    public interface IRootUrlsRepository
    {
        Task<List<RootUrls>> GetRootUrlsAsync();
        Task<RootUrls?> GetByIdAsync(int id);
    }
}
