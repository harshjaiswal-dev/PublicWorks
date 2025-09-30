using Business.DTOs;
using Data.Model;

namespace Business.Service.Interface
{
    public interface IStatusService
    {
        Task<IEnumerable<Status>> GetStatusAsync();
        Task<Status> GetStatusByIdAsync(int id);
        Task CreateStatusAsync(StatusDto status);
        Task UpdateStatusAsync(int id, StatusDto status);
        Task DeleteStatusAsync(int id);
    }
}