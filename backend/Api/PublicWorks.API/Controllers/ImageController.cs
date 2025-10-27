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

       

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var image = await _service.GetImageByIdAsync(id);
            if (image == null)
                return NotFound();
            return Ok(image);
        }

       

    }
}