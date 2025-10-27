using Data.GenericRepository;
using Data.Model;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Implementations
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(AppDbContext context) : base(context)
        {
            
        }
    
    }
}