using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Model
{
    /// <summary>
    /// Represents an image associated with an Issue.
    /// </summary>
    public class IssueImage
    {
        /// <summary>
        /// Primary key for the IssueImage entity.
        /// </summary>
        [Key]
        public int ImageId { get; set; }

        /// <summary>
        /// Foreign key referencing the associated Issue.
        /// </summary>
        [Required(ErrorMessage = "Issue reference is required.")]
        public int IssueId { get; set; }

        /// <summary>
        /// Navigation property for the associated Issue.
        /// </summary>
        [ForeignKey(nameof(IssueId))]
        public Issue? Issue { get; set; }

        /// <summary>
        /// Path or URL to the stored image.
        /// Maximum length 255 characters.
        /// </summary>
        [Required(ErrorMessage = "Image path is required.")]
        [MaxLength(255, ErrorMessage = "Image path cannot exceed 255 characters.")]
        public string ImagePath { get; set; } = string.Empty;

        /// <summary>
        /// Timestamp when the image was uploaded.
        /// Defaults to current UTC time.
        /// </summary>
        [Required]
        public DateTimeOffset UploadedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}