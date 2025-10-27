using Business.DTOs;
using Business.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace PublicWorks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _service;
        public MessageController(IMessageService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var messages = await _service.GetMessagesAsync();
            return Ok(messages);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var message = await _service.GetMessageByIdAsync(id);
            if (message == null)
                return NotFound();
            return Ok(message);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IssueMessageDto message)
        {
            await _service.CreateMessageAsync(message);
            return CreatedAtAction(nameof(GetById), new { id = message.MessageId }, message);
        }

        

    }
}