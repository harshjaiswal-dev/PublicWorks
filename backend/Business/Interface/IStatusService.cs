using Data.Model;

public interface IStatusService
{
    Task<IEnumerable<Status>> GetAllAsync();
    Task<Status?> GetByIdAsync(int id);
    Task AddAsync(Status message);
    Task UpdateAsync(Status message);
    Task DeleteAsync(int id);
}
