using Data.Model;
using Data.GenericRepository;

namespace Data.Interfaces
{
    public interface IStatusRepository : IGenericRepository<Status>
    {
 Task<IEnumerable<Status>> GetMessagesByUserIdAsync(int userId);
    }
  

}