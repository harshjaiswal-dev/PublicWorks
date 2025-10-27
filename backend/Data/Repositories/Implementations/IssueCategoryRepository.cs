using Data.GenericRepository;
using Data.Model;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Implementations
{
    public class IssueCategoryRepository : GenericRepository<IssueCategory>, IIssueCategoryRepository
    {
        public IssueCategoryRepository(AppDbContext context) : base(context)
        {

        }
    
    }
}