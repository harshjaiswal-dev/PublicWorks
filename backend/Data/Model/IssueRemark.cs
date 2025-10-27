using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Data.Model
{
    /// <summary>
    /// Represents a remark or comment added to an issue by a user.
    /// </summary>
    public class IssueRemark
    {
        /// <summary>
        /// Primary key for the IssueRemark entity.
        /// </summary>
        [Key]
        public int RemarkId { get; set; }

        /// <summary>
        /// Foreign key referencing the associated issue.
        /// </summary>
        [Required(ErrorMessage = "Issue reference is required.")]
        public int IssueId { get; set; }

        /// <summary>
        /// Navigation property for the associated issue.
        /// </summary>
        [ForeignKey(nameof(IssueId))]
        public Issue? Issue { get; set; }

        /// <summary>
        /// The text of the remark.
        /// Maximum 1000 characters.
        /// </summary>
        [Required(ErrorMessage = "Remark text is required.")]
        [MaxLength(1000, ErrorMessage = "Remark cannot exceed 1000 characters.")]
        public string RemarkText { get; set; } = string.Empty;

        /// <summary>
        /// Foreign key referencing the user who made the remark.
        /// </summary>
        [Required(ErrorMessage = "User who added the remark is required.")]
        public int RemarkedByUserId { get; set; }

        /// <summary>
        /// Navigation property for the user who added the remark.
        /// </summary>
        [DeleteBehavior(DeleteBehavior.Restrict)]
        [ForeignKey(nameof(RemarkedByUserId))]
        public User? RemarkBy { get; set; } 

        /// <summary>
        /// Timestamp when the remark was added.
        /// Defaults to current UTC time.
        /// </summary>
        [Required]
        public DateTimeOffset RemarkedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}