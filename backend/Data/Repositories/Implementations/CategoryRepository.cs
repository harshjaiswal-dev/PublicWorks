using Data;
using Data.GenericRepository;
using Data.Interfaces;
using Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Implementations.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
    //     private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context) : base(context)
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