using Data.Model;

public interface IMessageService
{
    Task<IEnumerable<Message>> GetAllAsync();
    Task<Message?> GetByIdAsync(int id);
    Task AddAsync(Message message);
    Task UpdateAsync(Message message);
    Task DeleteAsync(int id);
}
