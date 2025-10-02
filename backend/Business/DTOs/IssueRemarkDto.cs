using System.ComponentModel.DataAnnotations;

namespace Business.DTOs
{
    /// <summary>
    /// Data Transfer Object for issue remarks.
    /// </summary>
    public class IssueRemarkDto
    {
        /// <summary>
        /// Primary key of the remark.
        /// </summary>
        public int RemarkId { get; set; }

        /// <summary>
        /// Foreign key of the related issue.
        /// </summary>
        [Required(ErrorMessage = "IssueId is required.")]
        public int IssueId { get; set; }

        /// <summary>
        /// The text content of the remark.
        /// </summary>
        [Required(ErrorMessage = "Remark text is required.")]
        [MaxLength(1000, ErrorMessage = "Remark text cannot exceed 1000 characters.")]
        public string RemarkText { get; set; } = string.Empty;

        /// <summary>
        /// User ID of the person who made the remark.
        /// </summary>
        [Required(ErrorMessage = "User who added the remark is required.")]
        public int RemarkedByUserId { get; set; }

        /// <summary>
        /// Date and time when the remark was made.
        /// </summary>
        [Required(ErrorMessage = "Remark date/time is required.")]
        public DateTimeOffset RemarkedAt { get; set; }
    }
}