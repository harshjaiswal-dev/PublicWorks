using Business.DTOs;
using Data.Model;

namespace Business.Service.Interface
{
    public interface IImageService
    {
        Task<IEnumerable<Image>> GetImagesAsync();
        Task<Image> GetImageByIdAsync(int id);
        Task CreateImageAsync(ImageDto image);
        Task UpdateImageAsync(int id, ImageDto image);
        Task DeleteImageAsync(int id);
    }
}