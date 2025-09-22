using Data.Model;

namespace Business.Service.Interface
{
    public interface IRemarkService
    {
        Task<IEnumerable<Remark>> GetRemarksAsync();
        Task<Remark> GetRemarkByIdAsync(int id);
        Task CreateRemarkAsync(Remark remark);
        Task UpdateRemarkAsync(int id, Remark remark);
        Task DeleteRemarkAsync(int id);
    }
}