using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Model
{
    /// <summary>
    /// Represents a message related to an issue, sent between users.
    /// </summary>
    public class IssueMessage
    {
        /// <summary>
        /// Primary key for the IssueMessage entity.
        /// </summary>
        [Key]
        public int MessageId { get; set; }

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
        /// Foreign key referencing the user who sent the message.
        /// </summary>
        [Required(ErrorMessage = "Sender is required.")]
        public int SentByUserId { get; set; }

        /// <summary>
        /// Navigation property for the user who sent the message.
        /// </summary>
        [ForeignKey(nameof(SentByUserId))]
        public User? Sender { get; set; } = null!;

        /// <summary>
        /// Foreign key referencing the user who receives the message.
        /// </summary>
        [Required(ErrorMessage = "Recipient is required.")]
        public int SentToUserId { get; set; }

        /// <summary>
        /// Navigation property for the recipient user.
        /// </summary>
        [ForeignKey(nameof(SentToUserId))]
        public User? Receiver { get; set; } 

        /// <summary>
        /// Subject of the message.
        /// Maximum 255 characters.
        /// </summary>
        [Required(ErrorMessage = "Subject is required.")]
        [MaxLength(255, ErrorMessage = "Subject cannot exceed 255 characters.")]
        public string Subject { get; set; } = string.Empty;

        /// <summary>
        /// Body content of the message.
        /// Maximum 500 characters.
        /// </summary>
        [Required(ErrorMessage = "Message body is required.")]
        [MaxLength(500, ErrorMessage = "Message body cannot exceed 500 characters.")]
        public string Body { get; set; } = string.Empty;

        /// <summary>
        /// Timestamp when the message was sent.
        /// Defaults to current UTC time.
        /// </summary>
        [Required]
        public DateTimeOffset SentAt { get; set; } = DateTimeOffset.UtcNow;
    }
}