using SpeedApply.Api.Dtos;
using SpeedApply.Api.Interfaces;

namespace SpeedApply.Api.Services
{
    public class RootUrlsService : IRootUrlsService
    {
        private readonly IRootUrlsRepository _repository;

        public RootUrlsService(IRootUrlsRepository repository)
        {
            _repository = repository;
        }

        public async Task<RootUrlsDto?> GetRootUrlByIdAsync(int id)
        {
            var rootUrl = await _repository.GetByIdAsync(id);
            if (rootUrl == null) return null;
            
            return new RootUrlsDto { 
                Id = rootUrl.Id, 
                Domain = rootUrl.Domain, 
                CreatedAt = rootUrl.CreatedAt
            };
        }
    }
}
