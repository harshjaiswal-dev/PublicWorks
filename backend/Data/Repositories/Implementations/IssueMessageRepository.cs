using Data.GenericRepository;
using Data.Model;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Implementations
{
    public class IssueMessageRepository : GenericRepository<IssueMessage>, IIssueMessageRepository
    {
        public IssueMessageRepository(AppDbContext context) : base(context)
        {
            
        }
        
    }
}