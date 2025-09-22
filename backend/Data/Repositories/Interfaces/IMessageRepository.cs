using Data.Model;
using Data.GenericRepository;

namespace Data.Interfaces
{
    public interface IMessageRepository : IGenericRepository<Message>
    {
 Task<IEnumerable<Message>> GetMessagesByUserIdAsync(int userId);
    }
  

}