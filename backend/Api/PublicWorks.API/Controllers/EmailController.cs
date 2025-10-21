using Business.DTOs;
using Business.Service.Implementation;
using Business.Service.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PublicWorks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly EmailService _emailService;

        public EmailController()
        {

            _emailService = new EmailService("publicworksad1@gmail.com", "sknwdidbybatssxd");
        }

        [HttpPost("SendToUser")]
        public async Task<IActionResult> SendToUser([FromBody] EmailRequest request)
        {
            try
            {
                await _emailService.SendEmailAsync(request.ToEmail, request.Subject, request.Body);
                return Ok("Email sent successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to send email: {ex.Message}");
            }
        }
    }

    public class EmailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }

}




