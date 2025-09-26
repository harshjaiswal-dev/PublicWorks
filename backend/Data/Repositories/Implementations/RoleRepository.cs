using Data;
using Data.GenericRepository;
using Data.Model;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Implementations.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
    //     private readonly AppDbContext _context;

        public RoleRepository(AppDbContext context) : base(context)
        {
           //_context = context;
        }
    //      public async Task<IEnumerable<Status>> GetMessagesByUserIdAsync(int userId)
    // {
    //     return await _context.Status
            
    //         .ToListAsync();
    // }
    }
}