using Business.DTOs;
using Data.Model;

namespace Business.Service.Interface
{
    public interface ICategoryService
    {
        Task<IEnumerable<IssueCategory>> GetCategoryAsync();
        Task<IssueCategory> GetCategoryByIdAsync(int id);
        // Task CreateCategoryAsync(CategoryDto category);
        // Task UpdateCategoryAsync(int id, CategoryDto category);
        // Task DeleteCategoryAsync(int id);
    }
}