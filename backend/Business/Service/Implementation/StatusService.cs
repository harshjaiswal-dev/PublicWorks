using Business.DTOs;
using Business.Service.Interface;
using Data.Model;
using Data.UnitOfWork;

namespace Business.Service.Implementation
{
    public class StatusService : IStatusService
    {
        private readonly IUoW _unitOfWork;

        public StatusService(IUoW unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Status>> GetStatusAsync()
        {
            return await _unitOfWork.StatusRepository.GetAllAsync();
        }

        public async Task<Status> GetStatusByIdAsync(int id)
        {
            return await _unitOfWork.StatusRepository.GetByIdAsync(id);
        }

        public async Task CreateStatusAsync(StatusDto dto)
        {
            var status = new Status()
            {
                StatusId=dto.StatusId,
                Name=dto.Name,
                Description=dto.Description
             
            };

            await _unitOfWork.StatusRepository.AddAsync(status);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateStatusAsync(int id, StatusDto dto)
        {
            var status = new Status()
            {
               StatusId=dto.StatusId,
                Name=dto.Name,
                Description=dto.Description
            };

            await _unitOfWork.StatusRepository.UpdateAsync(id, status);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteStatusAsync(int id)
        {
            await _unitOfWork.StatusRepository.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }
    }
}