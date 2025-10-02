using Business.DTOs;
using Data.Model;

namespace Business.Service.Interface
{
    public interface IStatusService
    {
        Task<IEnumerable<IssueStatus>> GetStatusAsync();
        Task<IssueStatus> GetStatusByIdAsync(int id);
        // Task CreateStatusAsync(StatusDto status);
        // Task UpdateStatusAsync(int id, StatusDto status);
        // Task DeleteStatusAsync(int id);
    }
}