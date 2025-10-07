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
        
        public void Update(Issue issue)
        {
            // Attach the entity if it’s not being tracked
            if (_context.Entry(issue).State == EntityState.Detached)
            {
                _context.Issue.Attach(issue);
            }

            // Mark the entity as modified
            _context.Entry(issue).State = EntityState.Modified;
        }

    }
}