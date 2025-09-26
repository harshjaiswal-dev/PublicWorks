using Microsoft.AspNetCore.Mvc;

namespace PublicWorks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // private readonly IAuthService _authService;

        // public AuthController(IAuthService authService)
        // {
        //     _authService = authService;
        // } 

        // [HttpPost("login")]
        // public async Task<IActionResult> Login(LoginRequestDto request)
        // {
        //     try
        //     {
        //         var res = await _authService.LoginAsync(request);
        //         return Ok(res);
        //     }
        //     catch (Exception ex)
        //     {
        //         return BadRequest(ex.Message);
        //     }
        // }

        // [HttpPost("google")]
        // public async Task<IActionResult> GoogleLogin(GoogleLoginRequestDto request)
        // {
        //     try
        //     {
        //         var res = await _authService.GoogleLoginAsync(request);
        //         return Ok(res);
        //     }
        //     catch (Exception ex)
        //     {
        //         return BadRequest(ex.Message);
        //     }
        // }
    }
}
