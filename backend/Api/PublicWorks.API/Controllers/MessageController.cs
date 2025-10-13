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

        // [HttpPost]
        // public async Task<IActionResult> Create([FromBody] IssueMessageDto message)
        // {
        //     await _service.CreateMessageAsync(message);
        //     return CreatedAtAction(nameof(GetById), new { id = message.MessageId }, message);
        // }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IssueMessageDto message)
        {
            if (message == null)
                return BadRequest("Message cannot be null.");

            await _service.CreateMessageAsync(message);
            return CreatedAtAction(nameof(GetById), new { id = message.MessageId }, message);
        }


        // [HttpPut("{id}")]
        // public async Task<IActionResult> Update(int id, [FromBody] MessageDto message)
        // {
        //     if (id != message.ID)
        //         return BadRequest();

        //     await _service.UpdateMessageAsync(id, message);
        //     return NoContent();
        // }

        // [HttpDelete("{id}")]
        // public async Task<IActionResult> Delete(int id)
        // {
        //     await _service.DeleteMessageAsync(id);
        //     return NoContent();
        // }

    }
}