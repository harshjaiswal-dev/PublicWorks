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

        public AuthController(IGoogleAuthService googleAuthService, JwtService jwtService)
        {
            _googleAuthService = googleAuthService;
            _jwtService = jwtService;
        }

        [HttpPost("callback")]
        public async Task<IActionResult> GoogleCallback([FromQuery] string code)
        {
            Console.WriteLine("CODE IS HERE");
            
            var user = await _googleAuthService.HandleGoogleLoginAsync(code);

            // Create JWT for your app
            var appJwt = _jwtService.CreateToken(user.UserId.ToString(), user.Name);

            return Ok(new
            {
                token = appJwt,
                user = new { user.UserId, user.Name, user.ProfilePicture, user.RoleId }
            });
            // Redirect to frontend with JWT as query param
        }
    }
}

