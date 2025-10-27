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


    }
}