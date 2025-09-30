// Business/Service/IAuthService.cs
using Business.DTOs;
using Data.Model;
using System.Threading.Tasks;

namespace Business.Service.Interface
{
    public interface IAuthService
    {
        Task<object> AdminLoginAsync(string username, string password);
    }
}
