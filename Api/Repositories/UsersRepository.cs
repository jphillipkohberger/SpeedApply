using SpeedApply.Api.Interfaces;
using SpeedApply.Api.Models;
using SpeedApply.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace SpeedApply.Api.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly AppDbContext _context;

        public UsersRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Users?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task<Users?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<List<Users>> GetUsersAsync()    
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<Users> CreateUserAsync(Users user)
        {
            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<Users> UpdateUserAsync(Users user)
        {
            _context.Update(user);

            await _context.SaveChangesAsync();

            return user;
        }
    }
}
