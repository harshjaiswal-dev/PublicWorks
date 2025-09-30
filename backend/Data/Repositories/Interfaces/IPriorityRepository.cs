using Data.Model;
using Data.GenericRepository;

namespace Data.Interfaces
{
    public interface IPriorityRepository : IGenericRepository<Priority>
    {
        //Task<IEnumerable<Status>> GetMessagesByUserIdAsync(int userId);
    }
  

}