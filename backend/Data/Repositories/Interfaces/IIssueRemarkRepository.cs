using Data.Model;
using Data.GenericRepository;

namespace Data.Repositories.Interfaces
{
    public interface IIssueRemarkRepository : IGenericRepository<IssueRemark>
    {
        Task<IEnumerable<IssueRemark>> GetRemarksbyIssueIdAsync(int issueId);
    }
}

