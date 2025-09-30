namespace Business.Service.Interface
{
    public interface IJwtService
    {
        string GenerateAccessToken(string userId, string email);
        string GenerateRefreshToken();
    }
}