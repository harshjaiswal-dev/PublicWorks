using Data.Model;

namespace Business.Service.Interface
{
    public interface IAuthService
    {
        Task<User> AdminLoginAsync(string username, string password);
    }
}