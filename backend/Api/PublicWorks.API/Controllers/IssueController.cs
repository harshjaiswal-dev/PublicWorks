using Business.DTOs;
using Business.Service.Interface;
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
        private readonly IUserHelper _userHelper;
        private readonly IIssueService _service;
        public IssueController(IIssueService service, IUserHelper userHelper)
        {
            _service = service;
            _userHelper = userHelper;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? statusId, [FromQuery] int? priorityId)
        {
            var issues = await _service.GetIssuesAsync(statusId, priorityId);
            return Ok(issues);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var issue = await _service.GetIssueByIdAsync(id);
            if (issue == null)
                return NotFound();
            return Ok(issue);
        }

        [Authorize(Roles = "User")]
        [HttpPost("submit")]
        public async Task<IActionResult> SubmitIssue([FromForm] IssueCreateDto dto)
        {
            var userId = _userHelper.GetLoggedInUserId() ?? 0;
            dto.UserId = userId;

            int issueId = await _service.SubmitIssueAsync(dto);

            return Ok(new SubmitIssueResponse
            {
                Success = true,
                Message = "Issue submitted successfully",
                IssueId = issueId
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("summary")]
        public async Task<IActionResult> GetIssueSummary()
        {
            var summary = await _service.GetIssueSummaryAsync();
            return Ok(summary);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-issue/{issueId}")]
        public async Task<IActionResult> UpdateIssue(int issueId, [FromBody] UpdateIssueDto dto)
        {
            try
            {
                // Call the service to update the status
                var updatedIssue = await _service.UpdateIssueAsync(issueId, dto.StatusId, dto.PriorityId, dto.CategoryId);

                if (updatedIssue == null)
                    return NotFound(new UpdateIssueResponse 
                    { Message = "Issue not found" });

                return Ok(new  UpdateIssueResponse { IssueId = updatedIssue.IssueId, StatusId = updatedIssue.StatusId });
            }
            catch (Exception ex)
            {
                // Optional: log the error
                Log.Error(ex, "Error updating issue status");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }
        
        [Authorize(Roles = "Admin")]
        [HttpGet("{issueId}/images")]
        public async Task<IActionResult> GetIssueImages(int issueId)
        {
            var images = await _service.GetIssueImagesAsync(issueId);
            if (images == null || !images.Any())
                return NotFound(new IssueImagesResponse { Message = "No images found for this issue.",Data=null });

            return Ok(images);
        }
    }
}