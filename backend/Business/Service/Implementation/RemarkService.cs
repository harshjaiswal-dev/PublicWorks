using Business.DTOs;
using Business.Service.Interface;
using Data.Model;
using Data.UnitOfWork;
using Microsoft.AspNetCore.Http;

namespace Business.Service.Implementation
{
    public class RemarkService : IRemarkService
    {
        private readonly IUnitOfWork _unitOfWork;  private readonly IHttpContextAccessor _httpContextAccessor;

        public RemarkService(IUnitOfWork unitOfWork,IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<IssueRemark>> GetRemarksAsync()
        {
            return await _unitOfWork.IssueRemarkRepository.GetAllAsync();
        }

        public async Task<IssueRemark> GetRemarkByIdAsync(int id)
        {
            return await _unitOfWork.IssueRemarkRepository.GetByIdAsync(id);
        }
private int GetLoggedInUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null) throw new Exception("User not authenticated.");

            var claim = user.FindFirst("UserId");
            if (claim == null) throw new Exception("UserId claim missing.");

            if (!int.TryParse(claim.Value, out int userId))
                throw new Exception("Invalid UserId claim.");

            return userId;
        }

        public async Task CreateRemarkAsync(IssueRemarkDto dto)
        {
            var loggedInUserId = GetLoggedInUserId();
             

            var remark = new IssueRemark()
            {
                RemarkId = dto.RemarkId,
                IssueId = dto.IssueId,
                RemarkText = dto.RemarkText,
                RemarkedByUserId = loggedInUserId,
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