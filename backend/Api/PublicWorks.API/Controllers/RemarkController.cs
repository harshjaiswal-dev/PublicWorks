using Business.DTOs;
using Business.Service.Interface;
using Data.Model;
using Microsoft.AspNetCore.Mvc;

namespace PublicWorks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RemarkController : ControllerBase
    {
        private readonly IRemarkService _service;
        public RemarkController(IRemarkService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var remarks = await _service.GetRemarksAsync();
            return Ok(remarks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var remark = await _service.GetRemarkByIdAsync(id);
            if (remark == null)
                return NotFound();
            return Ok(remark);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RemarkDto remark)
        {
            await _service.CreateRemarkAsync(remark);
            return CreatedAtAction(nameof(GetById), new { id = remark.ID }, remark);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RemarkDto remark)
        {
            if (id != remark.ID)
                return BadRequest();

            await _service.UpdateRemarkAsync(id, remark);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteRemarkAsync(id);
            return NoContent();
        }

    }
}