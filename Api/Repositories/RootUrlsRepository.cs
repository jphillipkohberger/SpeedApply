using SpeedApply.Api.Interfaces;
using SpeedApply.Api.Models;
using SpeedApply.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace SpeedApply.Api.Repositories
{
    public class RootUrlsRepository : IRootUrlsRepository
    {
        private readonly AppDbContext _context;

        public RootUrlsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<RootUrls?> GetByIdAsync(int id)
        {
            return await _context.RootUrls.FindAsync(id);
        }
    }
}
