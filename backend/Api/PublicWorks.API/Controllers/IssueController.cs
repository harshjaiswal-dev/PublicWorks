using Business.DTOs;
using Business.Service.Interface;
using Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace PublicWorks.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class IssueController : ControllerBase
    {
        private readonly IIssueService _service;
        private readonly IWebHostEnvironment _env;
        public IssueController(IIssueService service, IWebHostEnvironment env)
        {
            _service = service;
            _env = env;
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

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitIssue([FromForm] IssueCreateDto dto)
        {
            
            int issueId = await _service.SubmitIssueAsync(dto);

            // Log.Information("Issue submitted successfully by user {User}. IssueId={IssueId}", 
            //         User.Identity?.Name ?? "Anonymous", issueId);

            return Ok(new
            {
                Success = true,
                Message = "Issue submitted successfully",
                IssueId = issueId
            });
        }
    }
}