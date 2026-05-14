using SpeedApply.Api.Interfaces;
using SpeedApply.Api.Models;
using SpeedApply.Api.Data;

namespace SpeedApply.Api.Repositories
{
    public class QueriesRepository : IQueriesRepository
    {
        private readonly AppDbContext _context;

        public QueriesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Queries?> GetByIdAsync(int id)
        {
            return await _context.Queries.FindAsync(id);
        }
    }
}
