using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Model
{
    /// <summary>
    /// Represents an audit log entry for tracking changes to entities.
    /// Stores which user performed the action, what entity was changed, and old/new values.
    /// </summary>
    public class AuditTrail
    {
        /// <summary>
        /// Primary key for the AuditTrail entry.
        /// </summary>
        [Key]
        public int AuditTrailId { get; set; }

        /// <summary>
        /// Foreign key referencing the user who performed the action.
        /// Nullable in case the action was performed by a system process.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Navigation property for the user who performed the action.
        /// </summary>
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }

        /// <summary>
        /// Name of the entity/table that was modified (e.g., "Issue", "EmailMessage").
        /// </summary>
        [Required(ErrorMessage = "Entity name is required.")]
        [MaxLength(100)]
        public string EntityName { get; set; } = string.Empty;

        /// <summary>
        /// Primary key of the record that was modified in the target entity.
        /// </summary>
        [Required(ErrorMessage = "Record Id is required.")]
        public int RecordId { get; set; }

        /// <summary>
        /// Foreign key referencing the type of action performed (Insert, Update, Delete, etc.).
        /// </summary>
        [Required(ErrorMessage = "Action type is required.")]
        public int ActionTypeId { get; set; }

        /// <summary>
        /// Navigation property for the action type.
        /// </summary>
        [ForeignKey(nameof(ActionTypeId))]
        public ActionType? ActionType { get; set; }

        /// <summary>
        /// JSON string representing the old values before the action.
        /// </summary>
        public string OldValues { get; set; } = string.Empty;

        /// <summary>
        /// JSON string representing the new values after the action.
        /// </summary>
        public string NewValues { get; set; } = string.Empty;

        /// <summary>
        /// Timestamp when the action occurred.
        /// </summary>
        [Required]
        public DateTimeOffset ActionDate { get; set; } = DateTimeOffset.UtcNow;
    }
}