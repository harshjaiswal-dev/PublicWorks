using Data.GenericRepository;
using Data.Model;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Implementations
{
    public class IssueStatusRepository : GenericRepository<IssueStatus>, IIssueStatusRepository
    {
        public IssueStatusRepository(AppDbContext context) : base(context)
        {

        }
    
    }
}