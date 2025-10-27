using Business.DTOs;
using Business.Service.Interface;
using Data.Model;
using Data.UnitOfWork;

namespace Business.Service.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<IssueCategory>> GetCategoryAsync()
        {
            return await _unitOfWork.IssueCategoryRepository.GetAllAsync();
        }

        public async Task<IssueCategory> GetCategoryByIdAsync(int id)
        {
            return await _unitOfWork.IssueCategoryRepository.GetByIdAsync(id);
        }

       
    }
}