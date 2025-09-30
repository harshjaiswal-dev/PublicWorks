using Business.DTOs;
using Data.Model;

namespace Business.Service.Interface
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategoryAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task CreateCategoryAsync(CategoryDto category);
        Task UpdateCategoryAsync(int id, CategoryDto category);
        Task DeleteCategoryAsync(int id);
    }
}