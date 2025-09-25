using Business.DTOs;
using Business.Helpers;
using Business.Service.Interface;
using Data.Model;
using Data.UnitOfWork;

namespace Business.Service.Implementation
{
    public class IssueService : IIssueService
    {
        private readonly IUnitOfWork _unitOfWork;

        public IssueService(IUnitOfWork unitOfWork)
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

        public async Task<int> SubmitIssueAsync(IssueCreateDto issueDto)
        {
            if (issueDto == null) throw new ArgumentNullException(nameof(issueDto));

            int issueId = 0;
            var uploadedFilePaths = new List<string>();

            // await using var transaction = await _dbContext.Database.BeginTransactionAsync();
            // try
            // {
                // Create Issue
                var issue = new Issue
                {
                    UserId = issueDto.UserId,
                    CategoryId = issueDto.CategoryId,
                    Lat = issueDto.Latitude,
                    Long = issueDto.Longitude,
                    Description = issueDto.Description,
                    PriorityId = 1,
                    StatusId = 1
                };

                await _unitOfWork.IssueRepository.AddAsync(issue);
                await _unitOfWork.SaveAsync();
                issueId = issue.IssueId;

                // Save Images
                if (issueDto.Images != null && issueDto.Images.Count > 0)
                {
                    foreach (var imageFile in issueDto.Images)
                    {
                        string relativePath = await FileHelper.SaveFileAsync(imageFile, issueDto.UserId, issueId);
                        uploadedFilePaths.Add(relativePath);

                        var image = new Image
                        {
                            IssueId = issueId,
                            ImagePath = relativePath,
                            UploadedAt = DateTimeOffset.UtcNow
                        };

                        await _unitOfWork.ImageRepository.AddAsync(image);
                    }

                    await _unitOfWork.SaveAsync();
                }

                // Commit Transaction
                // await transaction.CommitAsync();
                return issueId;
        }
        
        // public async Task CreateIssueAsync(IssueDto dto)
        // {
        //     var issue = new Issue()
        //     {
        //         IssueId = dto.IssueId,
        //         UserId = dto.UserId,
        //         CategoryId = dto.CategoryId,
        //         Lat = dto.Lat,
        //         Long = dto.Long,
        //         Description = dto.Description,
        //         PriorityId = dto.PriorityId,
        //         StatusId = dto.StatusId
        //     };

        //     await _unitOfWork.IssueRepository.AddAsync(issue);
        //     await _unitOfWork.SaveAsync();
        // }

        // public async Task UpdateIssueAsync(int id, IssueDto dto)
        // {
        //     var issue = new Issue()
        //     {
        //         IssueId = dto.IssueId,
        //         UserId = dto.UserId,
        //         CategoryId = dto.CategoryId,
        //         Lat = dto.Lat,
        //         Long = dto.Long,
        //         Description = dto.Description,
        //         PriorityId = dto.PriorityId,
        //         StatusId = dto.StatusId
        //     };

        //     await _unitOfWork.IssueRepository.UpdateAsync(id, issue);
        //     await _unitOfWork.SaveAsync();
        // }

        // public async Task DeleteIssueAsync(int id)
        // {
        //     await _unitOfWork.IssueRepository.DeleteAsync(id);
        //     await _unitOfWork.SaveAsync();
        // }
    }
}