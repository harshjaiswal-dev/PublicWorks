using Business.DTOs;
using Business.Service.Interface;
using Data.Model;
using Microsoft.AspNetCore.Mvc;

namespace PublicWorks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PriorityController : ControllerBase
    {
        private readonly IPriorityService _service;
        public PriorityController(IPriorityService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var remarks = await _service.GetPriorityAsync();
            return Ok(remarks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var remark = await _service.GetPriorityByIdAsync(id);
            if (remark == null)
                return NotFound();
            return Ok(remark);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PriorityDto priority)
        {
            await _service.CreatePriorityAsync(priority);
            return CreatedAtAction(nameof(GetById), new { id = priority.PriorityId}, priority);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PriorityDto priority)
        {
            if (id != priority.PriorityId)
                return BadRequest();

            await _service.UpdatePriorityAsync(id, priority);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeletePriorityAsync(id);
            return NoContent();
        }

    }
}