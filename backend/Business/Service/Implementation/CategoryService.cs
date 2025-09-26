using Business.DTOs;
using Business.Service.Interface;
using Data.Model;
using Data.UnitOfWork;

namespace Business.Service.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly IUoW _unitOfWork;

        public CategoryService(IUoW unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Category>> GetCategoryAsync()
        {
            return await _unitOfWork.CategoryRepository.GetAllAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _unitOfWork.CategoryRepository.GetByIdAsync(id);
        }

        public async Task CreateCategoryAsync(CategoryDto dto)
        {
            var category = new Category()
            {
              CategoryId=dto.CategoryId,
                Name=dto.Name,
                Description=dto.Description
             
            };

            await _unitOfWork.CategoryRepository.AddAsync(category);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateCategoryAsync(int id, CategoryDto dto)
        {
            var category = new Category()
            {
            CategoryId=dto.CategoryId,
                Name=dto.Name,
                Description=dto.Description
            };

            await _unitOfWork.CategoryRepository.UpdateAsync(id, category);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await _unitOfWork.PriorityRepository.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }
    }
}