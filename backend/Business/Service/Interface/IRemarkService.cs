using Business.DTOs;
using Data.Model;

namespace Business.Service.Interface
{
    public interface IRemarkService
    {
        //Task<IEnumerable<IssueRemark>> GetRemarksAsync();
        Task<IssueRemark> GetRemarkByIdAsync(int id);
        Task CreateRemarkAsync(IssueRemarkDto remark);
        // Task UpdateRemarkAsync(int id, RemarkDto remark);
        // Task DeleteRemarkAsync(int id);
        Task<IEnumerable<IssueRemark>> GetRemarksbyIssueIdAsync(int issueId);
    }
}