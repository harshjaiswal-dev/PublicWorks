using Google.Apis.Auth;
using Data.Model;
using Business.Service.Interface;
using Business.DTOs;
using Microsoft.Extensions.Configuration;
using Data.GenericRepository;

namespace Business.Service.Implementation
{
    public class GoogleAuthService : IGoogleAuthService
    {
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpFactory;
        private readonly IGenericRepository<User> _userRepo;

        public GoogleAuthService(IConfiguration config, IHttpClientFactory httpFactory, IGenericRepository<User> userRepo)
        {
            _config = config;
            _httpFactory = httpFactory;
            _userRepo = userRepo;
        }

        public async Task<User> HandleGoogleLoginAsync(string code)
        {
            var client = _httpFactory.CreateClient();

            var values = new Dictionary<string, string>
            {
                ["code"] = code,
                ["client_id"] = _config["Authentication:Google:ClientId"],
                ["client_secret"] = _config["Authentication:Google:ClientSecret"],
                ["redirect_uri"] = _config["Authentication:Google:RedirectUri"],
                ["grant_type"] = "authorization_code"
            };
            Console.WriteLine("Values: " + string.Join(", ", values.Select(kvp => $"{kvp.Key}={kvp.Value}")));

            var response = await client.PostAsync(
                "https://oauth2.googleapis.com/token",
                new FormUrlEncodedContent(values));

            var json = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
                throw new Exception("Google token exchange failed: " + json);

            var tokens = System.Text.Json.JsonSerializer.Deserialize<GoogleTokenResponseDto>(json,
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            // Validate id_token
            var payload = await GoogleJsonWebSignature.ValidateAsync(tokens.id_token,
                new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { _config["Authentication:Google:ClientId"] }
                });

            // Check if user exists
            // var allUsers = await _userRepo.GetAllAsync();
            // var user = allUsers.FirstOrDefault(u => u.GoogleUserId == payload.Subject);
            var user = await _userRepo.GetByConditionAsync(u => u.GoogleUserId == payload.Subject);
            // user.LastLoginAt = DateTimeOffset.UtcNow;
            // await _userRepo.UpdateAsync(user.UserId, user);

            if (user == null)
            {
                // Create new user
                user = new User
                {
                    GoogleUserId = payload.Subject,
                    Name = payload.Name ?? payload.Email,
                    RoleId = 2, // 2 = User role
                    ProfilePicture = payload.Picture,
                    LastLoginAt = DateTimeOffset.UtcNow,
                    IsActive = true
                };
                Console.WriteLine("THIS IS USER OBJ {0}", user);
                await _userRepo.AddAsync(user);
                Console.WriteLine("THIS IS USER22 OBJ {0}", user);

            }
            else
            {
                // Update last login
                user.LastLoginAt = DateTimeOffset.UtcNow;
                await _userRepo.UpdateAsync(user.UserId, user);
                Console.WriteLine($"Updated LastLoginAt: {user.LastLoginAt}");
            }

            return user;
        }
    }
}
