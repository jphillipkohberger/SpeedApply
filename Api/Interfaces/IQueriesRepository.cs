using SpeedApply.Api.Models;

namespace SpeedApply.Api.Interfaces
{
    public interface IQueriesRepository
    {
        Task<Queries?> GetByIdAsync(int id);
    }
}
