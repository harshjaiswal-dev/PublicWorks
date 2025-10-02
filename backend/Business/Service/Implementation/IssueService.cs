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
        //private readonly GeometryFactory _geometryFactory;

        public IssueService(IUnitOfWork unitOfWork/*, GeometryFactory geometryFactory*/)
        {
            _unitOfWork = unitOfWork;
            // _geometryFactory = geometryFactory ?? throw new ArgumentNullException(nameof(geometryFactory));
        }

        public async Task<IEnumerable<Issue>> GetIssuesAsync()
        {
            return await _unitOfWork.IssueRepository.GetAllAsync();
        }

        public async Task<Issue> GetIssueByIdAsync(int id)
        {
            return await _unitOfWork.IssueRepository.GetByIdAsync(id);
        }

        //         public async Task<int> SubmitIssueAsync(IssueCreateDto issueDto)
        //         {
        //             if (issueDto == null) throw new ArgumentNullException(nameof(issueDto));
        //  var locationPoint = _geometryFactory.CreatePoint(new Coordinate(issueDto.Longitude, issueDto.Latitude));
        //             int issueId = 0;
        //             var uploadedFilePaths = new List<string>();

        //             // await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        //             // try
        //             // {
        //                 // Create Issue
        //                 var issue = new Issue
        //                 {
        //                     UserId = issueDto.UserId,
        //                     CategoryId = issueDto.CategoryId,
        //                  Location = locationPoint,
        //                     Description = issueDto.Description,
        //                     PriorityId = 1,
        //                     StatusId = 1
        //                 };

        //                 await _unitOfWork.IssueRepository.AddAsync(issue);
        //                 await _unitOfWork.SaveAsync();
        //                 issueId = issue.IssueId;

        //                 // Save Images
        //                 if (issueDto.Images != null && issueDto.Images.Count > 0)
        //                 {
        //                     foreach (var imageFile in issueDto.Images)
        //                     {
        //                         string relativePath = await FileHelper.SaveFileAsync(imageFile, issueDto.UserId, issueId);
        //                         uploadedFilePaths.Add(relativePath);

        //                         var image = new Image
        //                         {
        //                             IssueId = issueId,
        //                             ImagePath = relativePath,
        //                             UploadedAt = DateTimeOffset.UtcNow
        //                         };

        //                         await _unitOfWork.ImageRepository.AddAsync(image);
        //                     }

        //                     await _unitOfWork.SaveAsync();
        //                 }

        //                 // Commit Transaction
        //                 // await transaction.CommitAsync();
        //                 return issueId;
        //         }

        public async Task<int> SubmitIssueAsync(IssueCreateDto issueDto)
        {
            if (issueDto == null) throw new ArgumentNullException(nameof(issueDto));

            // Create geography Point from Longitude and Latitude (longitude first!)
            // var locationPoint = _geometryFactory.CreatePoint(new Coordinate(issueDto.Longitude, issueDto.Latitude));
            //  locationPoint.SRID = 4326;
            var issue = new Issue
            {
                ReporterUserId = 2,
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
                    string relativePath = await FileHelper.SaveFileAsync(imageFile, issueDto.UserId, issueId);
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
    }
}