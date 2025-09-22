using Data.Interfaces;
using Data.Model;

public class StatusService : IStatusService
{
    private readonly IStatusRepository _repository;

    public StatusService(IStatusRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Status>> GetAllAsync() => await _repository.GetAllAsync();
    public async Task<Status?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);
    public async Task AddAsync(Status status)
    {
        await _repository.AddAsync(status);
        await _repository.SaveAsync();
    }



    public async Task UpdateAsync(Status status)
    {
        var existing = await _repository.GetByIdAsync(status.StatusId);
        if (existing == null) return;

        // Update only changed fields
        if (existing.Name != status.Name)
            existing.Name = status.Name;

        if (existing.Description != status.Description)
            existing.Description = status.Description;

        await _repository.UpdateAsync(existing); // this is now tracked
        await _repository.SaveAsync(); // triggers audit
    }
    // public async Task UpdateAsync(Status status)
    // {

    //     await _repository.UpdateAsync(status);
    //     await _repository.SaveAsync();
    // }

    public async Task DeleteAsync(int id)
    {
        var msg = await _repository.GetByIdAsync(id);
        if (msg != null)
        {
            _repository.Delete(msg);
            await _repository.SaveAsync();
        }
    }

    public async Task<IEnumerable<Status>> GetByUserAsync(int userId)
    {
        return await _repository.GetMessagesByUserIdAsync(userId);
    }
}
