using Data.Model;
using Data.GenericRepository;

namespace Data.Interfaces
{
    public interface IAuditTrailRepository : IGenericRepository<AuditTrail>
    {
        Task LogAsync(AuditTrail auditTrail);
    }
}