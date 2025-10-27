using System.ComponentModel.DataAnnotations;

namespace Data.Model
{
    /// <summary>
    /// Represents the status of an issue (e.g., Pending, In Progress, Resolved).
    /// </summary>
    public class IssueStatus
    {
        /// <summary>
        /// Primary key for the Status entity.
        /// </summary>
        [Key]
        public int StatusId { get; set; }

        /// <summary>
        /// Name of the status (e.g., "Pending", "Resolved").
        /// Required and maximum 50 characters.
        /// </summary>
        [Required(ErrorMessage = "Status name is required.")]
        [MaxLength(50, ErrorMessage = "Status name cannot exceed 50 characters.")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Optional description for the status.
        /// Maximum 255 characters.
        /// </summary>
        [MaxLength(255, ErrorMessage = "Description cannot exceed 255 characters.")]
        public string? Description { get; set; }
    }
}