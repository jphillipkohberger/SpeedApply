using SpeedApply.Api.Dtos;
using SpeedApply.Api.Interfaces;
using SpeedApply.Api.Models;

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

        public async Task<FilesDto?> SaveUserFileAsync(int id, string name)
        {
            var file = new Files { UserId = id, Name = name};

            // update database
            var fileSaved = await _repository.CreateFileAsync(file);
            if (fileSaved == null) return null;

            return new FilesDto { Id = id, Name = name };
        }
    }
}
