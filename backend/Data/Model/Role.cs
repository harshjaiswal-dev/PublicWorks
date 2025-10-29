using System.ComponentModel.DataAnnotations;

namespace Data.Model
{
    /// <summary>
    /// Represents a user role in the system (e.g., Admin, User, Moderator).
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Primary key for the Role entity.
        /// </summary>
        [Key]
        public int RoleId { get; set; }

        /// <summary>
        /// Name of the role (e.g., "Admin", "User").
        /// Required and maximum 50 characters.
        /// </summary>
        [Required(ErrorMessage = "Role name is required.")]
        [MaxLength(50, ErrorMessage = "Role name cannot exceed 50 characters.")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Optional description for the role.
        /// Maximum 255 characters.
        /// </summary>
        [MaxLength(255, ErrorMessage = "Description cannot exceed 255 characters.")]
        public string? Description { get; set; }
        
    }
}