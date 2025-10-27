using Google.Apis.Auth;
using Data.Model;

namespace Business.Service.Interface
{
    public interface IGoogleAuthService
    {
        Task<User> HandleGoogleLoginAsync(string code);
    }
}
