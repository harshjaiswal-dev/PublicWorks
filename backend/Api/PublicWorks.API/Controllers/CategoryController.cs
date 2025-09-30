using Business.DTOs;
using Business.Service.Interface;
using Data.Model;
using Microsoft.AspNetCore.Mvc;

namespace PublicWorks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;
        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var remarks = await _service.GetCategoryAsync();
            return Ok(remarks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var remark = await _service.GetCategoryByIdAsync(id);
            if (remark == null)
                return NotFound();
            return Ok(remark);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryDto category)
        {
            await _service.CreateCategoryAsync(category);
            return CreatedAtAction(nameof(GetById), new { id = category.CategoryId},  category
);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryDto category)
        {
            if (id != category.CategoryId)
                return BadRequest();

            await _service.UpdateCategoryAsync(id, category);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteCategoryAsync(id);
            return NoContent();
        }

    }
}