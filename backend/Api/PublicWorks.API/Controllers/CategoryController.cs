using System.Diagnostics;
using Business.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace PublicWorks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;
        private readonly ICacheService _cacheService;
        public CategoryController(ICategoryService service, ICacheService cacheService)
        {
            _cacheService = cacheService;
            _service = service;
        }

        // [HttpGet]
        // public async Task<IActionResult> GetAll()
        // {
        //     var stopwatch = new Stopwatch();
        //     stopwatch.Start();

        //     var remarks = await _service.GetCategoryAsync();
        //     stopwatch.Stop();

        //     Console.WriteLine($"[GetAll] Time taken (no caching): {stopwatch.ElapsedMilliseconds} ms");

        //     return Ok(remarks);
        // }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stopwatch = Stopwatch.StartNew();
            var cacheKey = "categoryList";

            var categories = await _cacheService.GetOrAddAsync(
                cacheKey,
                async () => await _service.GetCategoryAsync(),
                TimeSpan.FromMinutes(30)
            );
            stopwatch.Stop(); 
            Console.WriteLine($"[CategoryController] Total API time: {stopwatch.ElapsedMilliseconds} ms");

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var remark = await _service.GetCategoryByIdAsync(id);
            if (remark == null)
                return NotFound();
            return Ok(remark);
        }

//         [HttpPost]
//         public async Task<IActionResult> Create([FromBody] CategoryDto category)
//         {
//             await _service.CreateCategoryAsync(category);
//             return CreatedAtAction(nameof(GetById), new { id = category.CategoryId},  category
// );
//         }

//         [HttpPut("{id}")]
//         public async Task<IActionResult> Update(int id, [FromBody] CategoryDto category)
//         {
//             if (id != category.CategoryId)
//                 return BadRequest();

//             await _service.UpdateCategoryAsync(id, category);
//             return NoContent();
//         }

//         [HttpDelete("{id}")]
//         public async Task<IActionResult> Delete(int id)
//         {
//             await _service.DeleteCategoryAsync(id);
//             return NoContent();
//         }

    }
}