using Business.DTOs;
using Business.Service.Interface;
using Data.Model;
using Microsoft.AspNetCore.Mvc;

namespace PublicWorks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _service;
        public ImageController(IImageService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var images = await _service.GetImagesAsync();
            return Ok(images);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var image = await _service.GetImageByIdAsync(id);
            if (image == null)
                return NotFound();
            return Ok(image);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ImageDto image)
        {
            await _service.CreateImageAsync(image);
            return CreatedAtAction(nameof(GetById), new { id = image.ID }, image);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ImageDto image)
        {
            if (id != image.ID)
                return BadRequest();

            await _service.UpdateImageAsync(id, image);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteImageAsync(id);
            return NoContent();
        }

    }
}