using Business.Interface;
using Data;
using Data.Model;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class MessageController : ControllerBase
{
     private readonly IUnitOfWork _unitOfWork;
      
    public MessageController( IUnitOfWork unitofwork)
    {
      
        _unitOfWork = unitofwork;
    }

    

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      
        var messages = await _unitOfWork.Message.GetAllAsync();
        return Ok(messages);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {  
        var message = await _unitOfWork.Message.GetByIdAsync(id);
        return message == null ? NotFound() : Ok(message);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Message message)
    {
      
        await _unitOfWork.Message.AddAsync(message);
        await _unitOfWork.CompleteAsync();
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Message message)
    {
        
        if (id != message.ID) return BadRequest();
        await _unitOfWork.Message.UpdateAsync(message);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
       
       // await _unitOfWork.Messages.Delete(id);
        return Ok();
    }
}
