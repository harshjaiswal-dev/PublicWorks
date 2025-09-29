using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite.Geometries;

namespace Data.Model
{
    /// <summary>
    /// Represents a reported issue with spatial location, priority, and status.
    /// </summary>
    public class Issue
    {
        /// <summary>
        /// Primary key for the Issue entity.
        /// </summary>
        [Key]
        public int IssueId { get; set; }

        /// <summary>
        /// Foreign key referencing the user who reported the issue.
        /// </summary>
        [Required(ErrorMessage = "Reporting user is required.")]
        public int ReporterUserId { get; set; }

        /// <summary>
        /// Navigation property for the reporting user.
        /// </summary>
        [ForeignKey(nameof(ReporterUserId))]
        public User Reporter { get; set; } = null!;

        /// <summary>
        /// Foreign key referencing the category of the issue.
        /// </summary>
        [Required(ErrorMessage = "Category is required.")]
        public int IssueCategoryId { get; set; }

        /// <summary>
        /// Navigation property for the issue category.
        /// </summary>
        [ForeignKey(nameof(IssueCategoryId))]
        public IssueCategory Category { get; set; } = null!;

        /// <summary>
        /// Geographical location of the issue (latitude/longitude).
        /// Stored as SQL Server geography type.
        /// </summary>
        [Required(ErrorMessage = "Location is required.")]
        [Column(TypeName = "geography")]
        public Point Location { get; set; } = null!;

        /// <summary>
        /// Detailed description of the issue.
        /// Maximum 1000 characters.
        /// </summary>
        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Foreign key referencing the priority of the issue.
        /// This will be set automatically by AI/ML before saving.
        /// </summary>
        [Required]
        public int PriorityId { get; set; }

        /// <summary>
        /// Navigation property for the issue's priority.
        /// </summary>
        [ForeignKey(nameof(PriorityId))]
        public IssuePriority Priority { get; set; } = null!;

        /// <summary>
        /// Foreign key referencing the status of the issue.
        /// Default status is 'Pending' when the issue is created.
        /// </summary>
        [Required]
        public int StatusId { get; set; }

        /// <summary>
        /// Navigation property for the issue's status.
        /// </summary>
        [ForeignKey(nameof(StatusId))]
        public IssueStatus Status { get; set; } = null!;

        /// <summary>
        /// Timestamp when the issue was created.
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}