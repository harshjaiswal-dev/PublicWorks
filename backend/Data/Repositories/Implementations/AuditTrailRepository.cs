using Data;
using Data.GenericRepository;
using Data.Interfaces;
using Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Implementations.Repositories
{
    public class AuditTrailRepository : GenericRepository<AuditTrail>, IAuditTrailRepository
    {
        private readonly AppDbContext _context;

        public AuditTrailRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}