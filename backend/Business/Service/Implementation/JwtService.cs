using Business.Service.Interface;
using Business.Service.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Business.Service.Implementation
{
    /// <summary>
    /// Provides functionality for generating JSON Web Tokens (JWT) for authentication.
    /// Includes both short-lived access tokens and long-lived refresh tokens.
    /// </summary>
    public class JwtService : IJwtService
    {
        private readonly JwtSettings _jwtSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtService"/> class.
        /// JWT configuration is injected using IOptions pattern.
        /// </summary>
        /// <param name="jwtSettings">The strongly-typed JWT configuration.</param>
        public JwtService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings?.Value ?? throw new ArgumentNullException(nameof(jwtSettings));
        }

        /// <summary>
        /// Generates a short-lived access token for a user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="email">The email of the user.</param>
        /// <param name="role">The role of the user (default: "User").</param>
        /// <returns>A signed JWT access token.</returns>
        public string GenerateAccessToken(string userId, string email)
        {
            if (string.IsNullOrWhiteSpace(_jwtSettings.Key))
                throw new InvalidOperationException("JWT Key is missing in configuration.");

            // Create a symmetric security key from the secret
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

            // Create signing credentials using HMAC-SHA256 algorithm
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Define claims for the JWT token
            // var claims = new[]
            // {
            //     new Claim(JwtRegisteredClaimNames.Sub, userId),   // User identifier
            //     new Claim(JwtRegisteredClaimNames.Email, email), // User email
            //     new Claim(ClaimTypes.Role, role)                 // User role
            // };

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim("role", "User")
            };

            // Create the JWT token
            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpiryMinutes),
                signingCredentials: signingCredentials
            );

            // Serialize and return the token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Generates a secure, random refresh token.
        /// Refresh tokens are long-lived and used to request new access tokens.
        /// </summary>
        /// <returns>A cryptographically secure refresh token string.</returns>
        public string GenerateRefreshToken()
        {
            var randomBytes = new byte[64]; // 512-bit token
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);

            // Convert to Base64 string for storage or transmission
            return Convert.ToBase64String(randomBytes);
        }
    }
}
