using Data.GenericRepository;
using Data.Model;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Implementations
{
    public class IssueRemarkRepository : GenericRepository<IssueRemark>, IIssueRemarkRepository
    {
        public IssueRemarkRepository(AppDbContext context) : base(context)
        {

        }
        
        public async Task<IEnumerable<IssueRemark>> GetRemarksbyIssueIdAsync(int issueId)
        {
            return await _context.IssueRemark
                .Where(u => u.IssueId == issueId)
                .ToListAsync();
        } 
    }
}