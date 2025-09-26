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

        public async Task<IEnumerable<Image>> GetImagesAsync()
        {
            return await _unitOfWork.ImageRepository.GetAllAsync();
        }

        public async Task<Image> GetImageByIdAsync(int id)
        {
            return await _unitOfWork.ImageRepository.GetByIdAsync(id);
        }

        // public async Task CreateImageAsync(ImageDto dto)
        // {
        //     var image = new Image()
        //     {
        //         ID = dto.ID,
        //         IssueId = dto.IssueId,
        //         ImagePath = dto.ImagePath
        //     };

        //     await _unitOfWork.ImageRepository.AddAsync(image);
        //     await _unitOfWork.SaveAsync();
        // }

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