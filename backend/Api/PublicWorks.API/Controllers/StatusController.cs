using Data;
using Data.Model;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class StatusController : ControllerBase
{
   
    private readonly IStatusService _service;
 private readonly ILogger<StatusController> _logger;

    public StatusController(IStatusService service, ILogger<StatusController> logger)
    {

        _service = service;
        _logger = logger;
    }

    

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all status messages");
        var messages = await _service.GetAllAsync();
        return Ok(messages);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        _logger.LogInformation("Getting status message with ID {StatusId}", id);
        var message = await _service.GetByIdAsync(id);
        if (message == null)
        {
            _logger.LogWarning("Status message with ID {StatusId} not found", id);
            return NotFound();
        }
            return Ok(message);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Status status)
    {
      _logger.LogInformation("Creating a new status message");
        await _service.AddAsync(status);
        _logger.LogInformation("Status message created with ID {StatusId}", status.StatusId);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Status status)
    {
        
        if (id != status.StatusId)
        {
            _logger.LogWarning("Status ID in URL ({UrlId}) does not match status ID in body ({BodyId})", id, status.StatusId);
            return BadRequest();
        }

        _logger.LogInformation("Updating status message with ID {StatusId}", id);
        await _service.UpdateAsync(status);
        _logger.LogInformation("Status message with ID {StatusId} updated", id);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
       
        _logger.LogInformation("Deleting status message with ID {StatusId}", id);
        await _service.DeleteAsync(id);
        _logger.LogInformation("Status message with ID {StatusId} deleted", id);
        return Ok();
    }
}
