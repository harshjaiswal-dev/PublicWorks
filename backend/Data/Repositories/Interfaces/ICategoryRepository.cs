using Data.Model;
using Data.GenericRepository;

namespace Data.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        //Task<IEnumerable<Status>> GetMessagesByUserIdAsync(int userId);
    }
  

}