using Business.Service.Interface;
using Data.Model;
using Data.UnitOfWork;

namespace Business.Service.Implementation
{
    public class RemarkService : IRemarkService
    {
        private readonly IUoW _unitOfWork;

        public RemarkService(IUoW unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Remark>> GetRemarksAsync()
        {
            return await _unitOfWork.RemarkRepository.GetAllAsync();
        }

        public async Task<Remark> GetRemarkByIdAsync(int id)
        {
            return await _unitOfWork.RemarkRepository.GetByIdAsync(id);
        }

        public async Task CreateRemarkAsync(Remark remark)
        {
            await _unitOfWork.RemarkRepository.AddAsync(remark);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateRemarkAsync(int id, Remark remark)
        {
            await _unitOfWork.RemarkRepository.UpdateAsync(id, remark);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteRemarkAsync(int id)
        {
            await _unitOfWork.RemarkRepository.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }
    }
}