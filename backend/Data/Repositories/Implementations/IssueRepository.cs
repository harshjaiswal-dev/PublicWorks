using System.Linq.Expressions;
using Data.GenericRepository;
using Data.Model;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Implementations
{
    public class IssueRepository : GenericRepository<Issue>, IIssueRepository
    {
        public IssueRepository(AppDbContext context) : base(context)
        {

        }
        
        // --- Summary methods ---
        public async Task<int> CountAsync()
        {
            return await _context.Issue.CountAsync();
        }

        public async Task<int> CountByConditionAsync(Expression<Func<Issue, bool>> predicate)
        {
            return await _context.Issue.CountAsync(predicate);
        }
    }
}