using Business.Service.Interface;
using Data.Model;
using Data.UnitOfWork;

namespace Business.Service.Implementation
{
    public class ActionTypeService : IActionTypeService
    {
        private readonly IUoW _unitOfWork;

        public ActionTypeService(IUoW unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ActionType>> GetActionTypesAsync()
        {
            return await _unitOfWork.ActionTypeRepository.GetAllAsync();
        }

        public async Task<ActionType> GetActionTypeByIdAsync(int id)
        {
            return await _unitOfWork.ActionTypeRepository.GetByIdAsync(id);
        }

        public async Task CreateActionTypeAsync(ActionType actionType)
        {
            await _unitOfWork.ActionTypeRepository.AddAsync(actionType);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateActionTypeAsync(int id, ActionType actionType)
        {
            await _unitOfWork.ActionTypeRepository.UpdateAsync(id, actionType);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteActionTypeAsync(int id)
        {
            await _unitOfWork.ActionTypeRepository.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }
    }
}