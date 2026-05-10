using SpeedApply.Api.Models;

namespace SpeedApply.Api.Interfaces
{
    public interface IRootUrlsRepository
    {
        Task<RootUrls?> GetByIdAsync(int id);
    }
}
