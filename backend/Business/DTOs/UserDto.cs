using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite.Geometries;

namespace Business.DTOs
{
    public class UserDto
    {
        /// <summary>
        /// Unique identifier of the user.
        /// </summary>
        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { get; set; }

        /// <summary>
        /// Google authentication user ID (if the user logs in with Google).
        /// </summary>
        [StringLength(50, ErrorMessage = "GoogleUserId cannot exceed 50 characters.")]
        public string? GoogleUserId { get; set; }

        /// <summary>
        /// Full name of the user.
        /// </summary>
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Email address of the user.
        /// </summary>
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Secure hash of the user’s password (optional for Google login).
        /// </summary>
        [StringLength(255, ErrorMessage = "Password hash length is invalid.")]
        public string? PasswordHash { get; set; }

        /// <summary>
        /// User phone number (optional, must be exactly 10 digits).
        /// </summary>
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Profile picture URL of the user.
        /// </summary>
        [StringLength(255, ErrorMessage = "Profile picture path cannot exceed 255 characters.")]
        public string? ProfilePicture { get; set; }

        /// <summary>
        /// Role identifier assigned to the user.
        /// </summary>
        [Required(ErrorMessage = "RoleId is required.")]
        public int RoleId { get; set; }

        /// <summary>
        /// Last login timestamp of the user.
        /// </summary>
        public DateTimeOffset? LastLoginAt { get; set; }

        /// <summary>
        /// User account creation timestamp.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        /// <summary>
        /// Indicates whether the user is active.
        /// </summary>
        public bool IsActive { get; set; } = true;
    
    }
}