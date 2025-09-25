using Business.Service.Interface;
using Data.Model;
using Microsoft.AspNetCore.Mvc;
using Google.Apis.Auth;

namespace PublicWorks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("google")]
        public async Task<IActionResult> GoogleLogin([FromBody] TokenRequest request)
        {
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(request.Token,
                    new GoogleJsonWebSignature.ValidationSettings()
                    {
                        Audience = new[] { _config["GoogleAuth:ClientId"] }
                    });

                var user = new
                {
                    email = payload.Email,
                    name = payload.Name,
                    picture = payload.Picture
                };

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

    }
    public class TokenRequest
    {
        public string Token { get; set; }
    }
}