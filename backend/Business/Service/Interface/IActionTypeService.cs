using Data.Model;

namespace Business.Service.Interface
{
    public interface IActionTypeService
    {
        Task<IEnumerable<ActionType>> GetActionTypesAsync();
        Task<ActionType> GetActionTypeByIdAsync(int id);
        Task CreateActionTypeAsync(ActionType actionType);
        Task UpdateActionTypeAsync(int id, ActionType actionType);
        Task DeleteActionTypeAsync(int id);
    }
}