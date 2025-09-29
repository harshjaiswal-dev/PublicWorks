using Data.Model;
using Data.GenericRepository;

namespace Data.Repositories.Interfaces
{
    public interface IMessageRepository : IGenericRepository<Message>
    {
        Task<IEnumerable<Message>> GetMessagesByUserIdAsync(int userId);
    }
  

}