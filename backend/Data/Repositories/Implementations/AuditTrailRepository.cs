using Data.GenericRepository;
using Data.Model;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Implementations
{
    public class AuditTrailRepository : GenericRepository<AuditTrail>, IAuditTrailRepository
    {
        public AuditTrailRepository(AppDbContext context) : base(context)
        {

        }
    }
}