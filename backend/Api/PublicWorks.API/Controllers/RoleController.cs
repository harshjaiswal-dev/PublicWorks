using Business.DTOs;
using Business.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace PublicWorks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _service;
        public RoleController(IRoleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var messages = await _service.GetRoleAsync();
            return Ok(messages);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var message = await _service.GetRoleByIdAsync(id);
            if (message == null)
                return NotFound();
            return Ok(message);
        }

       

    }
}