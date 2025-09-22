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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoleDto roles)
        {
            await _service.CreateRoleAsync(roles);
            return CreatedAtAction(nameof(GetById), new { id = roles.RoleId }, roles);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RoleDto roles)
        {
            if (id != roles.RoleId)
                return BadRequest();

            await _service.UpdateRoleAsync(id, roles);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteRoleAsync(id);
            return NoContent();
        }

    }
}