using Business.DTOs;
using Business.Service.Interface;
using Data.Model;
using Data.UnitOfWork;

namespace Business.Service.Implementation
{
    public class IssueService : IIssueService
    {
        private readonly IUoW _unitOfWork;

        public IssueService(IUoW unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Issue>> GetIssuesAsync()
        {
            return await _unitOfWork.IssueRepository.GetAllAsync();
        }

        public async Task<Issue> GetIssueByIdAsync(int id)
        {
            return await _unitOfWork.IssueRepository.GetByIdAsync(id);
        }

        public async Task CreateIssueAsync(IssueDto dto)
        {
            var issue = new Issue()
            {
                IssueId = dto.IssueId,
                UserId = dto.UserId,
                CategoryId = dto.CategoryId,
                Lat = dto.Lat,
                Long = dto.Long,
                Description = dto.Description,
                PriorityId = dto.PriorityId,
                StatusId = dto.StatusId
            };

            await _unitOfWork.IssueRepository.AddAsync(issue);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateIssueAsync(int id, IssueDto dto)
        {
            var issue = new Issue()
            {
                IssueId = dto.IssueId,
                UserId = dto.UserId,
                CategoryId = dto.CategoryId,
                Lat = dto.Lat,
                Long = dto.Long,
                Description = dto.Description,
                PriorityId = dto.PriorityId,
                StatusId = dto.StatusId
            };

            await _unitOfWork.IssueRepository.UpdateAsync(id, issue);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteIssueAsync(int id)
        {
            await _unitOfWork.IssueRepository.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }
    }
}