using Business.DTOs;
using Business.Service.Interface;
using Data.Model;
using Data.UnitOfWork;

namespace Business.Service.Implementation
{
    public class RemarkService : IRemarkService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemarkService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // public async Task<IEnumerable<IssueRemark>> GetRemarksAsync()
        // {
        //     return await _unitOfWork.IssueRemarkRepository.GetAllAsync();
        // }

        public async Task<IssueRemark> GetRemarkByIdAsync(int id)
        {
            return await _unitOfWork.IssueRemarkRepository.GetByIdAsync(id);
        }

        public async Task CreateRemarkAsync(IssueRemarkDto dto)
        {
            var remark = new IssueRemark()
            {
                RemarkId = dto.RemarkId,
                IssueId = dto.IssueId,
                RemarkText = dto.RemarkText,
                RemarkedByUserId = dto.RemarkedByUserId,
                RemarkedAt = dto.RemarkedAt
            };

            await _unitOfWork.IssueRemarkRepository.AddAsync(remark);
            await _unitOfWork.SaveAsync();
        }

        // public async Task UpdateRemarkAsync(int id, RemarkDto dto)
        // {
        //     var remark = new Remark()
        //     {
        //         ID = dto.ID,
        //         IssueId = dto.IssueId,
        //         RemarkText = dto.RemarkText,
        //         RemarkBy = dto.RemarkBy,
        //         RemarkAt = dto.RemarkAt
        //     };

        //     await _unitOfWork.RemarkRepository.UpdateAsync(id, remark);
        //     await _unitOfWork.SaveAsync();
        // }

        // public async Task DeleteRemarkAsync(int id)
        // {
        //     await _unitOfWork.RemarkRepository.DeleteAsync(id);
        //     await _unitOfWork.SaveAsync();
        // }

        public async Task<IEnumerable<IssueRemark>> GetRemarksbyIssueIdAsync(int issueId)
        {
            return await _unitOfWork.IssueRemarkRepository.GetRemarksbyIssueIdAsync(issueId);
        }
    }
}