using Data.Model;
using Data.GenericRepository;

namespace Data.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<IEnumerable<User>> GetUsersByRoleIdAsync(int roleId);
        Task<IEnumerable<User>> GetUsersByRoleNameAsync(string roleName);
    }
}