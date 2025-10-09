using System.ComponentModel.DataAnnotations;

namespace Business.DTOs
{
    /// <summary>
    /// Data Transfer Object for issue images.
    /// </summary>
    public class IssueImageDto
    {
        /// <summary>
        /// Unique identifier for the image.
        /// </summary>
        public int ImageId { get; set; }

        /// <summary>
        /// Identifier of the related issue.
        /// </summary>
        [Required(ErrorMessage = "IssueId is required.")]
        public int IssueId { get; set; }

        /// <summary>
        /// Path or URL of the uploaded image.
        /// </summary>
        [Required(ErrorMessage = "Image path is required.")]
        [StringLength(255, ErrorMessage = "Image path cannot exceed 255 characters.")]
        public string ImagePath { get; set; } = string.Empty;

        /// <summary>
        /// Date and time when the image was uploaded.
        /// </summary>
        public DateTimeOffset UploadedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}
