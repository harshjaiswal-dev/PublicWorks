using Data.Model;
using Data.GenericRepository;

namespace Data.Repositories.Interfaces
{
    public interface IIssueMessageRepository : IGenericRepository<IssueMessage>
    {
        Task<IEnumerable<IssueMessage>> GetMessagesbyIssueIdAsync(int issueId);
    }
  
}