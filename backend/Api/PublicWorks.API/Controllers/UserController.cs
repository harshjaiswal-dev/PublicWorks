using System.Security.Claims;
using Business.DTOs;
using Business.Service.Interface;
using Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PublicWorks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet("profile")]
        [Authorize] 
        public IActionResult GetProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var name = User.Identity?.Name;

            return Ok(new { message = "Welcome!", userId, name });
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _service.GetUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _service.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserDto user)
        {
            await _service.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = user.UserId }, user);
        }

       
    }
}