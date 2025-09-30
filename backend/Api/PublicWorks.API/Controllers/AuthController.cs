using Business.DTOs;
using Business.Service;
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
            var appJwt = _jwtService.CreateToken(user.UserId.ToString(), user.Name, user.RoleId == 1 ? "Admin" : "User");

            return Ok(new
            {
                token = appJwt,
                user = new { user.UserId, user.Name, user.ProfilePicture, user.RoleId }
            });
            // Redirect to frontend with JWT as query param
        }

        // [Authorize(Roles = "Admin")]
        [HttpPost("adminauth")]
        public async Task<IActionResult> AdminLogin([FromBody] AdminLoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.AdminLoginAsync(loginDto.Username, loginDto.Password);

            if (result == null)
                return Unauthorized(new { message = "Invalid username or password" });

            return Ok(result);
        }
        
    }
}

