using Microsoft.EntityFrameworkCore;
using SpeedApply.Api.Data;
using SpeedApply.Api.Interfaces;
using SpeedApply.Api.Models;

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

        public async Task<Users?> GetUserWithQueriesAsync(int userId)
        {
            return await _context.Users
                .Include(u => u.Queries)
                .Where(u => u.Id == userId)
                .Select(u => new Users
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Password = u.Password,
                    Email = u.Email,
                    Queries = u.Queries.Select(q => new Queries
                    {
                        Id = q.Id,
                        Query = q.Query,
                        UserId = q.UserId,
                    }).ToList()
                })
                .FirstOrDefaultAsync();
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
