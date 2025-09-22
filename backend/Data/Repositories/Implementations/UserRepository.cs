using Data;
using Data.GenericRepository;
using Data.Interfaces;
using Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Implementations.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsersByRoleIdAsync(int roleId)
        {
            return await _context.Users
                .Include(u => u.Role)
                .Where(u => u.RoleId == roleId)
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUsersByRoleNameAsync(string roleName)
        {
            return await _context.Users
                .Include(u => u.Role)
                .Where(u => u.Name == roleName)
                .ToListAsync();
        }
    }
}