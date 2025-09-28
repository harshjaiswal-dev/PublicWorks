using Business.DTOs;
using Business.Service.Interface;
using Data.Model;
using Data.UnitOfWork;

namespace Business.Service.Implementation
{
    public class PriorityService : IPriorityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PriorityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Priority>> GetPriorityAsync()
        {
            return await _unitOfWork.PriorityRepository.GetAllAsync();
        }

        public async Task<Priority> GetPriorityByIdAsync(int id)
        {
            return await _unitOfWork.PriorityRepository.GetByIdAsync(id);
        }

        public async Task CreatePriorityAsync(PriorityDto dto)
        {
            var priority = new Priority()
            {
                PriorityId=dto.PriorityId,
                Name=dto.Name,
                Description=dto.Description
             
            };

            await _unitOfWork.PriorityRepository.AddAsync(priority);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdatePriorityAsync(int id, PriorityDto dto)
        {
            var priority = new Priority()
            {
               PriorityId=dto.PriorityId,
                Name=dto.Name,
                Description=dto.Description
            };

            await _unitOfWork.PriorityRepository.UpdateAsync(id, priority);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeletePriorityAsync(int id)
        {
            await _unitOfWork.PriorityRepository.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }
    }
}