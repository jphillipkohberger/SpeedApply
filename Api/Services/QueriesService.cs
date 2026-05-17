using SpeedApply.Api.Dtos;
using SpeedApply.Api.Interfaces;

namespace SpeedApply.Api.Services
{
    public class QueriesService : IQueriesService
    {
        private readonly IQueriesRepository _repository;

        public QueriesService(IQueriesRepository repository)
        {
            _repository = repository;
        }

        public async Task<QueriesDto?> GetQueryByIdAsync(int id)
        {
            var query = await _repository.GetByIdAsync(id);
            if (query == null) return null;
            
            return new QueriesDto { 
                Id = query.Id, 
                Query = query.Query, 
                CreatedAt = query.CreatedAt,
                UserId = query.UserId,
            };
        }
    }
}
