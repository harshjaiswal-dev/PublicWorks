using Business.DTOs;
using Data.Model;

namespace Business.Service.Interface
{
    public interface IPriorityService
    {
        Task<IEnumerable<IssuePriority>> GetPriorityAsync();
        Task<IssuePriority> GetPriorityByIdAsync(int id);
        // Task CreatePriorityAsync(PriorityDto priority);
        // Task UpdatePriorityAsync(int id, PriorityDto priority);
        // Task DeletePriorityAsync(int id);
    }
}