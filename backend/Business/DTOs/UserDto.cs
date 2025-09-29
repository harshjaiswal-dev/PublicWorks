using System.ComponentModel.DataAnnotations;

namespace Business.DTOs
{
    public class UserDto
    {
        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { get; set; }

        [StringLength(50, ErrorMessage = "GoogleUserId cannot exceed 50 characters.")]
        public string? GoogleUserId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "Password hash length is invalid.")]
        public string? PasswordHash { get; set; }
        public string? ProfilePicture { get; set; }
        public int RoleId { get; set; }  

      
          // ✅ Instead of a single RoleId, expose multiple roles
    // public List<RoleDto> Roles { get; set; } = new List<RoleDto>();       
    public DateTimeOffset? LastLoginAt { get; set; }  
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public bool IsActive { get; set; } = true;
    }
}