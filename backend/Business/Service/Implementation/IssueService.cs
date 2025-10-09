using Business.DTOs;
using Business.Helpers;
using Business.Service.Interface;
using Data.Model;
using Data.UnitOfWork;
using NetTopologySuite.Geometries;

namespace Business.Service.Implementation
{
    public class IssueService : IIssueService
    {
        private readonly IUnitOfWork _unitOfWork;

        public IssueService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Issue>> GetIssuesAsync(int? statusId, int? priorityId)
        {
            return await _unitOfWork.IssueRepository.GetAllByConditionAsync(i =>
                (!statusId.HasValue || i.StatusId == statusId) &&
                (!priorityId.HasValue || i.PriorityId == priorityId));
        }

        public async Task<Issue> GetIssueByIdAsync(int id)
        {
            return await _unitOfWork.IssueRepository.GetByIdAsync(id);
        }

        public async Task<int> SubmitIssueAsync(IssueCreateDto issueDto)
        {
            if (issueDto == null) throw new ArgumentNullException(nameof(issueDto));

            var issue = new Issue
            {
                ReporterUserId = issueDto.UserId,
                IssueCategoryId = issueDto.CategoryId,
                Location = new Point(issueDto.Longitude, issueDto.Latitude) { SRID = 4326 },
                Description = issueDto.Description,
                PriorityId = 1, // You can adjust or take from DTO
                StatusId = 1    // You can adjust or take from DTO
            };

            await _unitOfWork.IssueRepository.AddAsync(issue);
            await _unitOfWork.SaveAsync();

            int issueId = issue.IssueId;

            // Save Images
            if (issueDto.Images != null && issueDto.Images.Count > 0)
            {
                var uploadedFilePaths = new List<string>();

                foreach (var imageFile in issueDto.Images)
                {
                    //string relativePath = await FileHelper.SaveFileAsync(imageFile, issueDto.UserId, issueId);
                    string relativePath = await FileHelper.SaveFileAsync(imageFile);
                    uploadedFilePaths.Add(relativePath);

                    var image = new IssueImage
                    {
                        IssueId = issueId,
                        ImagePath = relativePath,
                        UploadedAt = DateTimeOffset.UtcNow
                    };

                    await _unitOfWork.IssueImageRepository.AddAsync(image);
                }

                await _unitOfWork.SaveAsync();
            }

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

        public async Task<IssueSummaryDto> GetIssueSummaryAsync()
        {
            var total = await _unitOfWork.IssueRepository.CountAsync();
            var pending = await _unitOfWork.IssueRepository.CountByConditionAsync(i => i.StatusId == 1);
            var inProgress = await _unitOfWork.IssueRepository.CountByConditionAsync(i => i.StatusId == 2);
            var resolved = await _unitOfWork.IssueRepository.CountByConditionAsync(i => i.StatusId == 3);
            var highPriority = await _unitOfWork.IssueRepository.CountByConditionAsync(i => i.PriorityId == 3);

            return new IssueSummaryDto
            {
                TotalIssues = total,
                Pending = pending,
                InProgress = inProgress,
                Resolved = resolved,
                HighPriority = highPriority
            };
        }

        public async Task<Issue?> UpdateIssueAsync(int issueId, int statusId, int priorityId, int categoryId)
        {
            // Use the repository from the UnitOfWork
            var issue = await _unitOfWork.IssueRepository.GetByIdAsync(issueId);
            if (issue == null) return null;
            if (statusId != 0)
            {
                issue.StatusId = statusId;
            }
            if (priorityId != 0)
            {
                issue.PriorityId = priorityId;
            }
            if (categoryId != 0)
            {
                issue.IssueCategoryId = categoryId;
            }

            _unitOfWork.IssueRepository.Update(issue);
            await _unitOfWork.SaveAsync();

            return issue;
        }
        
        public async Task<IEnumerable<IssueImageDto>> GetIssueImagesAsync(int issueId)
        {
            var images = await _unitOfWork.IssueImageRepository.GetImagesByIssueIdAsync(issueId);

            return images.Select(img => new IssueImageDto
            {
                ImageId = img.ImageId,
                IssueId = img.IssueId,
                ImagePath = img.ImagePath,
                UploadedAt = img.UploadedAt
            });
        }

    }
}