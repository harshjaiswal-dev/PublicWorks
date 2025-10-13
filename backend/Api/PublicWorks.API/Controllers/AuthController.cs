using Business.DTOs;
using Business.Service.Implementation;
using Business.Service.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PublicWorks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IGoogleAuthService _googleAuthService;
        private readonly JwtService _jwtService;
        private readonly IAuthService _authService;

        public AuthController(IGoogleAuthService googleAuthService, JwtService jwtService, IAuthService authService)
        {
            _googleAuthService = googleAuthService;
            _jwtService = jwtService;
            _authService = authService;
        }

        [HttpGet("callback")]
        public async Task<IActionResult> GoogleCallback([FromQuery] string code)
        {
            Console.WriteLine("CODE IS HERE");

            var user = await _googleAuthService.HandleGoogleLoginAsync(code);

            // Create JWT for your app
            var appJwt = _jwtService.GenerateAccessToken(user.UserId.ToString(), user.Name, "User");

            return Ok(new
            {
                token = appJwt,
                user = new { user.UserId, user.Name, user.ProfilePicture, user.RoleId }
            });
            // Redirect to frontend with JWT as query param
        }

        [HttpPost("adminauth")]
        public async Task<IActionResult> AdminLogin([FromBody] AdminLoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _authService.AdminLoginAsync(loginDto.Username, loginDto.Password);

            if (user == null)
                return Unauthorized(new { message = "Invalid username or password" });

            var appJwt = _jwtService.GenerateAccessToken(user.UserId.ToString(), user.Name, "Admin");

            return Ok(new
            {
                token = appJwt,
                user = new { user.UserId, user.Name, user.ProfilePicture, user.RoleId, user.Email }
            });
        }
            [HttpPost("logout")]
            public IActionResult Logout()
            {
                HttpContext.Session.Clear();
    
                return Ok(new { message = "Logout successful" });
            }
    
    }
}

