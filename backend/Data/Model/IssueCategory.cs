using System.ComponentModel.DataAnnotations;

namespace Data.Model
{
    /// <summary>
    /// Represents a category for issues, emails, or other entities in the system.
    /// </summary>
    public class IssueCategory
    {
        /// <summary>
        /// Primary key for the Category entity.
        /// </summary>
        [Key]
        public int CategoryId { get; set; }

        /// <summary>
        /// Name of the category (e.g., "Road", "IT Support").
        /// Required and maximum 100 characters.
        /// </summary>
        [Required(ErrorMessage = "Category name is required.")]
        [MaxLength(100, ErrorMessage = "Category name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Optional description for the category.
        /// Maximum 500 characters.
        /// </summary>
        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }
    }
}