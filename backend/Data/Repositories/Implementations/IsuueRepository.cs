using Data;
using Data.GenericRepository;
using Data.Interfaces;
using Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Implementations.Repositories
{
    public class IssueRepository : GenericRepository<Issue>, IIssueRepository
    {
        private readonly AppDbContext _context;

        public IssueRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}