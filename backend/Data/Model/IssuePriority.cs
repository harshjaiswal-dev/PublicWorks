using System.ComponentModel.DataAnnotations;

namespace Data.Model
{
    /// <summary>
    /// Represents the priority level of an issue.
    /// The value is typically assigned automatically by AI/ML.
    /// </summary>
    public class IssuePriority
    {
        /// <summary>
        /// Primary key for the Priority entity.
        /// </summary>
        [Key]
        public int PriorityId { get; set; }

        /// <summary>
        /// Name of the priority (e.g., "Low", "Medium", "High").
        /// Required and maximum 50 characters.
        /// </summary>
        [Required(ErrorMessage = "Priority name is required.")]
        [MaxLength(50, ErrorMessage = "Priority name cannot exceed 50 characters.")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Optional description for the priority level.
        /// Maximum 255 characters.
        /// </summary>
        [MaxLength(255, ErrorMessage = "Description cannot exceed 255 characters.")]
        public string? Description { get; set; }
    }
}