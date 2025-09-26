using Data;
using Data.GenericRepository;
using Data.Interfaces;
using Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Implementations.Repositories
{
    public class PriorityRepository : GenericRepository<Priority>, IPriorityRepository
    {
    //     private readonly AppDbContext _context;

        public PriorityRepository(AppDbContext context) : base(context)
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