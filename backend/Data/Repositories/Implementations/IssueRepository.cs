using Data.GenericRepository;
using Data.Model;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Repositories.Implementations
{
    public class IssueRepository : GenericRepository<Issue>, IIssueRepository
    {
        private readonly AppDbContext _context;

        public IssueRepository(AppDbContext context): base(context)
        {
            _context = context;
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
