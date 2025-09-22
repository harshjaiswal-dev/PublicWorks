using Business.DTOs;
using Data.Model;
namespace Business.Service.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task CreateUserAsync(UserDto user);
        Task UpdateUserAsync(int id, UserDto user);
        Task DeleteUserAsync(int id);
    }
}
