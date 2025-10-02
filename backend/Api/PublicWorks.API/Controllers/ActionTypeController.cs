using Business.Service.Interface;
using Data.Model;
using Microsoft.AspNetCore.Mvc;

namespace PublicWorks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActionTypeController : ControllerBase
    {
        private readonly IActionTypeService _service;
        public ActionTypeController(IActionTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var actionTypes = await _service.GetActionTypesAsync();
            return Ok(actionTypes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var actionType = await _service.GetActionTypeByIdAsync(id);
            if (actionType == null)
                return NotFound();
            return Ok(actionType);
        }

        // [HttpPost]
        // public async Task<IActionResult> Create([FromBody] ActionType actionType)
        // {
        //     await _service.CreateActionTypeAsync(actionType);
        //     return CreatedAtAction(nameof(GetById), new { id = actionType.ActionTypeId }, actionType);
        // }

        // [HttpPut("{id}")]
        // public async Task<IActionResult> Update(int id, [FromBody] ActionType actionType)
        // {
        //     if (id != actionType.ActionTypeId)
        //         return BadRequest();

        //     await _service.UpdateActionTypeAsync(id, actionType);
        //     return NoContent();
        // }

        // [HttpDelete("{id}")]
        // public async Task<IActionResult> Delete(int id)
        // {
        //     await _service.DeleteActionTypeAsync(id);
        //     return NoContent();
        // }

    }
}