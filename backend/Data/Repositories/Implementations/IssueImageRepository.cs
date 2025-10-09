using Data.GenericRepository;
using Data.Model;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Implementations
{
    public class IssueImageRepository : GenericRepository<IssueImage>, IIssueImageRepository
    {
        public IssueImageRepository(AppDbContext context) : base(context)
        {

        }
        public async Task<IEnumerable<IssueImage>> GetImagesByIssueIdAsync(int issueId)
        {
            return await _context.IssueImage
                .Where(img => img.IssueId == issueId)
                .OrderByDescending(img => img.UploadedAt)
                .ToListAsync();
        }
    }
}