using SpeedApply.Api.Interfaces;
using SpeedApply.Api.Models;
using SpeedApply.Api.Dtos;
using SpeedApply.Api.Data;

namespace SpeedApply.Api.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly AppDbContext _context;

        public UsersRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Users> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}
