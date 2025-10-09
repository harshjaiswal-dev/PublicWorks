using Business.DTOs;
using Business.Service.Interface;
using Data.Model;
using Microsoft.AspNetCore.Mvc;

namespace PublicWorks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _service;
        public StatusController(IStatusService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var remarks = await _service.GetStatusAsync();
            return Ok(remarks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var remark = await _service.GetStatusByIdAsync(id);
            if (remark == null)
                return NotFound();
            return Ok(remark);
        }

        // [HttpPost]
        // public async Task<IActionResult> Create([FromBody] StatusDto status)
        // {
        //     await _service.CreateStatusAsync(status);
        //     return CreatedAtAction(nameof(GetById), new { id = status.StatusId}, status);
        // }

        // [HttpPut("{id}")]
        // public async Task<IActionResult> Update(int id, [FromBody] StatusDto status)
        // {
        //     if (id != status.StatusId)
        //         return BadRequest();

        //     await _service.UpdateStatusAsync(id, status);
        //     return NoContent();
        // }

        // [HttpDelete("{id}")]
        // public async Task<IActionResult> Delete(int id)
        // {
        //     await _service.DeleteStatusAsync(id);
        //     return NoContent();
        // }

    }
}