using Business.DTOs;
using Business.Service.Interface;
using Data.Model;
using Data.UnitOfWork;

namespace Business.Service.Implementation
{
    public class ImageService : IImageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ImageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // public async Task<IEnumerable<IssueImage>> GetImagesAsync()
        // {
        //     return await _unitOfWork.IssueImageRepository.GetAllAsync();
        // }

        public async Task<IssueImage> GetImageByIdAsync(int id)
        {
            return await _unitOfWork.IssueImageRepository.GetByIdAsync(id);
        }

        public async Task CreateImageAsync(IssueImageDto dto)
        {
            var image = new IssueImage()
            {
                ImageId = dto.ImageId,
                IssueId = dto.IssueId,
                ImagePath = dto.ImagePath,
                UploadedAt = dto.UploadedAt
            };

            await _unitOfWork.IssueImageRepository.AddAsync(image);
            await _unitOfWork.SaveAsync();
        }

        // public async Task UpdateImageAsync(int id, ImageDto dto)
        // {
        //     var image = new Image()
        //     {
        //         ID = dto.ID,
        //         IssueId = dto.IssueId,
        //         ImagePath = dto.ImagePath
        //     };

        //     await _unitOfWork.ImageRepository.UpdateAsync(id, image);
        //     await _unitOfWork.SaveAsync();
        // }

        // public async Task DeleteImageAsync(int id)
        // {
        //     await _unitOfWork.ImageRepository.DeleteAsync(id);
        //     await _unitOfWork.SaveAsync();
        // }
    }
}