using System.ComponentModel.DataAnnotations;

namespace Business.DTOs
{
    public class AdminLoginDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}