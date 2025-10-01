namespace Business.Service.Interface
{
    public interface IAuthService
    {
        Task<object> AdminLoginAsync(string username, string password);
    }
}