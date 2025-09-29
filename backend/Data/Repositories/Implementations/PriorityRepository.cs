using Data.GenericRepository;
using Data.Model;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Implementations
{
    public class PriorityRepository : GenericRepository<Priority>, IPriorityRepository
    {
        public PriorityRepository(AppDbContext context) : base(context)
        {
            
        }
    
    }
}