using SpeedApply.Api.Dtos;
using SpeedApply.Api.Interfaces;

namespace SpeedApply.Api.Services
{
    public class FilesService : IFilesService
    {
        private readonly IFilesRepository _repository;

        public FilesService(IFilesRepository repository)
        {
            _repository = repository;
        }

        public async Task<FilesDto?> GetFileByIdAsync(int id)
        {
            var file = await _repository.GetByIdAsync(id);
            if (file == null) return null;
            
            return new FilesDto { 
                Id = file.Id, 
                Name = file.Name, 
                CreatedAt = file.CreatedAt,
                UserId = file.UserId,
            };
        }
    }
}
