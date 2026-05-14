
using SpeedApply.Api.Dtos;

namespace SpeedApply.Api.Interfaces
{
    public interface IQueriesService
    {
        Task<QueriesDto?> GetQueryByIdAsync(int id);
    }
}
