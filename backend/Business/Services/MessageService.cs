using Business.Interface;
using Data.Interfaces;
using Data.Model;

public class MessageService : IMessageService
{
    // private readonly IMessageRepository _repository;
    private readonly IUnitOfWork _unitofwork;

    public MessageService(IUnitOfWork unitOfWork)
    {
        _unitofwork = unitOfWork;
    }

    public async Task<IEnumerable<Message>> GetAllAsync() => await _unitofwork.Message.GetAllAsync();
    public async Task<Message?> GetByIdAsync(int id) => await _unitofwork.Message.GetByIdAsync(id);
    public async Task AddAsync(Message message)
    {
        await _unitofwork.Message.AddAsync(message);
        await _unitofwork.Message.SaveAsync();
        
    }

    public async Task UpdateAsync(Message message)
    {
        await _unitofwork.Message.UpdateAsync(message);
        await _unitofwork.Message.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var msg = await _unitofwork.Message.GetByIdAsync(id);
        if (msg != null)
        {
            _unitofwork.Message.Delete(msg);
            await _unitofwork.Message.SaveAsync();
        }
    }

    // public async Task<IEnumerable<Message>> GetByUserAsync(int userId)
    // {
    //     return await _unitofwork.Messages.GetMessagesByUserIdAsync(userId);
    // }
}
