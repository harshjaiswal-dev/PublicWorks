using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Model
{
    /// <summary>
    /// Represents a system user.
    /// Users can have roles and may log in via Google or locally.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Primary key for the user.
        /// </summary>
        [Key]
        public int UserId { get; set; }

        /// <summary>
        /// Google user ID if the user signs in via Google OAuth.
        /// Nullable for local users.
        /// </summary>
        [MaxLength(100)]
        public string? GoogleUserId { get; set; }

        /// <summary>
        /// Full name of the user.
        /// Required, max length 100.
        /// </summary>
        [Required(ErrorMessage = "User name is required.")]
        [MaxLength(100, ErrorMessage = "User name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Email address of the user.
        /// Required, max length 100.
        /// </summary>
        [Required(ErrorMessage = "Email is required.")]
        [MaxLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Phone number of the user.
        /// Optional, must be exactly 10 digits.
        /// </summary>
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Hashed password for local authentication.
        /// Nullable if user uses Google OAuth.
        /// </summary>
        [MaxLength(255)]
        public string? PasswordHash { get; set; }

        /// <summary>
        /// URL or path to the profile picture.
        /// Optional, max length 255.
        /// </summary>
        [MaxLength(255)]
        public string? ProfilePicture { get; set; }

        /// <summary>
        /// Foreign key to the user's role.
        /// </summary>
        [Required(ErrorMessage = "Role is required.")]
        public int RoleId { get; set; }

        /// <summary>
        /// Navigation property for the role.
        /// </summary>
        [ForeignKey(nameof(RoleId))]
        public Role? Role { get; set; }

        /// <summary>
        /// Timestamp of the last login.
        /// Nullable if user has never logged in.
        /// </summary>
        public DateTimeOffset? LastLoginAt { get; set; }

        /// <summary>
        /// Timestamp when the user was created.
        /// Defaults to current UTC time.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        /// <summary>
        /// Indicates whether the user is active.
        /// </summary>
        public bool IsActive { get; set; } = true;
    }
}