using SpeedApply.Api.Interfaces;
using SpeedApply.Api.Models;
using SpeedApply.Api.Data;

namespace SpeedApply.Api.Repositories
{
    public class FilesRepository : IFilesRepository
    {
        private readonly AppDbContext _context;

        public FilesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Files?> GetByIdAsync(int id)
        {
            return await _context.Files.FindAsync(id);
        }

        public async Task<Files> CreateFileAsync(Files file)
        {
            await _context.Files.AddAsync(file);

            await _context.SaveChangesAsync();

            return file;
        }
    }
}
