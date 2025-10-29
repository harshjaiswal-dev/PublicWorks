using System;
using Data.GenericRepository;
using Data.Model;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Implementations
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        // ✅ Get all users that have a specific RoleId
           public async Task<IEnumerable<User>> GetUsersByRoleIdAsync(int roleId)
        {
            return await _context.User
                .Include(u => u.Role)
                .Where(u => u.RoleId == roleId)
                .ToListAsync();
        }
        public async Task<IEnumerable<User>> GetUsersByRoleNameAsync(string roleName)
        {
            return await _context.User
                .Include(u => u.Role)
                .Where(u => u.Name == roleName)
                .ToListAsync();
        }
        
        public async Task<bool> ExistsAsync(int userId)
        {
            return await _dbSet.AnyAsync(u => u.UserId == userId);
        }

    }
}
