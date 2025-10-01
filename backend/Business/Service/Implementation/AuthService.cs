using Business.Service.Interface;
using Data.GenericRepository;
using Data.Model;

namespace Business.Service.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IGenericRepository<User> _userRepo;
        private readonly JwtService _jwtService;

        public AuthService(IGenericRepository<User> userRepo, JwtService jwtService)
        {
            _userRepo = userRepo;
            _jwtService = jwtService;
        }

        public async Task<object> AdminLoginAsync(string username, string password)
        {
            // Check if admin exists
            var user = await _userRepo.GetByConditionAsync(u => u.Name == username && u.RoleId == 1);
            if (user == null)
                return null;

            // Password check (plaintext for now; hash in production)
            if (user.PasswordHash != password)
                return null;

            // Generate JWT using JwtService
            var token = _jwtService.GenerateAccessToken(user.UserId.ToString(), user.Name, "Admin");

            return new
            {
                user = new { username = user.Name, role = "Admin" },
                token
            };
        }
    }
}