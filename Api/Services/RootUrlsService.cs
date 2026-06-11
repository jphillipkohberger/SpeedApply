using SpeedApply.Api.Dtos;
using SpeedApply.Api.Interfaces;
using SpeedApply.Api.Models;

namespace SpeedApply.Api.Services
{
    public class RootUrlsService : IRootUrlsService
    {
        private readonly IRootUrlsRepository _repository;

        public RootUrlsService(IRootUrlsRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<RootUrlsDto>> GetRootUrlsAsync()
        {
            List<RootUrls> rootUrls = await _repository.GetRootUrlsAsync();

            List<RootUrlsDto> rootUrlsDto = rootUrls.Select(u => new RootUrlsDto
            {
                Id = u.Id,
                Domain = u.Domain,
                CreatedAt = u.CreatedAt
            }).ToList();

            return rootUrlsDto;
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
