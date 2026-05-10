
using SpeedApply.Api.Dtos;

namespace SpeedApply.Api.Interfaces
{
    public interface IRootUrlsService
    {
        Task<RootUrlsDto?> GetRootUrlByIdAsync(int id);
    }
}
