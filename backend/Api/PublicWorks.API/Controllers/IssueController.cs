using System.IdentityModel.Tokens.Jwt;
using Business.DTOs;
using Business.Service.Interface;
using Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublicWorks.API.Helpers;
using Serilog;

namespace PublicWorks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IssueController : ControllerBase
    {
        private readonly UserHelper _userHelper;
        private readonly IIssueService _service;
        public IssueController(IIssueService service, UserHelper userHelper)
        {
            _service = service;
            _userHelper = userHelper;
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

        [Authorize]
        [HttpPost("submit")]
        public async Task<IActionResult> SubmitIssue([FromForm] IssueCreateDto dto)
        {
            var userId = _userHelper.GetLoggedInUserId() ?? 0;
            dto.UserId = userId;

            int issueId = await _service.SubmitIssueAsync(dto);

            return Ok(new
            {
                Success = true,
                Message = "Issue submitted successfully",
                IssueId = issueId
            });
        }
        
        [HttpGet("summary")]
        public async Task<IActionResult> GetIssueSummary()
        {
            var summary = await _service.GetIssueSummaryAsync();
            return Ok(summary);
        }
    }
}