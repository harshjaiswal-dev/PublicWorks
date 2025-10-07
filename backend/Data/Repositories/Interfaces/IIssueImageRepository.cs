using Data.Model;
using Data.GenericRepository;

namespace Data.Repositories.Interfaces
{
    public interface IIssueImageRepository : IGenericRepository<IssueImage>
    {
        Task<IEnumerable<IssueImage>> GetImagesByIssueIdAsync(int issueId);
    }
}