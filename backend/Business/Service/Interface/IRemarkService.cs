using Business.DTOs;
using Data.Model;

namespace Business.Service.Interface
{
    public interface IRemarkService
    {
        Task<IEnumerable<Remark>> GetRemarksAsync();
        Task<Remark> GetRemarkByIdAsync(int id);
        Task CreateRemarkAsync(RemarkDto remark);
        Task UpdateRemarkAsync(int id, RemarkDto remark);
        Task DeleteRemarkAsync(int id);
    }
}