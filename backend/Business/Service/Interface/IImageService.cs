using Business.DTOs;
using Data.Model;

namespace Business.Service.Interface
{
    public interface IImageService
    {
        // Task<IEnumerable<IssueImage>> GetImagesAsync();
        Task<IssueImage> GetImageByIdAsync(int id);
        Task CreateImageAsync(IssueImageDto image);
        // Task UpdateImageAsync(int id, ImageDto image);
        // Task DeleteImageAsync(int id);
        //Task<IEnumerable<IssueImage>> GetImagebyIssueIdAsync(int issueId);
    }
}