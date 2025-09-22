using Business.DTOs;
using Data.Model;

namespace Business.Service.Interface
{
    public interface IIssueService
    {
        Task<IEnumerable<Issue>> GetIssuesAsync();
        Task<Issue> GetIssueByIdAsync(int id);
        Task CreateIssueAsync(IssueDto issue);
        Task UpdateIssueAsync(int id, IssueDto issue);
        Task DeleteIssueAsync(int id);
    }
}