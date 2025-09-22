using Data.GenericRepository;
using Data.Model;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Implementations.Repositories
{
    public class AuditTrailRepository : GenericRepository<AuditTrail>, IAuditTrailRepository
    {
        public AuditTrailRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
       
public async Task LogAsync(AuditTrail auditTrail)
{
    //checks for user exists or not
     bool userExists = await _context.Users.AnyAsync(u => u.UserId == auditTrail.UserId);
    if (!userExists)
    {
        throw new ArgumentException($"UserId {auditTrail.UserId} does not exist.");
    }
    //checks for action type exists or not like create
    bool exists = await _context.ActionType.AnyAsync(a => a.ActionTypeId == auditTrail.ActionTypeId);
    if (!exists)
    {
        throw new ArgumentException($"Invalid ActionTypeId: {auditTrail.ActionTypeId} does not exist.");
    }
//here in the audit table it is added
    _context.AuditTrail.Add(auditTrail);
    await _context.SaveChangesAsync();
}

    }
}