using Business.DTOs;
using Business.Service.Interface;
using Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PublicWorks.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class IssueController : ControllerBase
    {
        private readonly IIssueService _service;
        public IssueController(IIssueService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var issues = await _service.GetIssuesAsync();
            return Ok(issues);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var issue = await _service.GetIssueByIdAsync(id);
            if (issue == null)
                return NotFound();
            return Ok(issue);
        }

        [HttpPost]
        
        public async Task<IActionResult> Create([FromBody] IssueDto issue)
        {
            await _service.CreateIssueAsync(issue);
            return CreatedAtAction(nameof(GetById), new { id = issue.IssueId }, issue);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] IssueDto issue)
        {
            if (id != issue.IssueId)
                return BadRequest();

            await _service.UpdateIssueAsync(id, issue);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteIssueAsync(id);
            return NoContent();
        }

    }
}