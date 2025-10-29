using System.ComponentModel.DataAnnotations;

namespace Business.DTOs
{
    /// <summary>
    /// Data Transfer Object for transferring message information.
    /// </summary>
    public class IssueMessageDto
    {
        /// <summary>
        /// Unique identifier for the message.
        /// </summary>
        public int MessageId { get; set; }

        /// <summary>
        /// Identifier of the related issue.
        /// </summary>
        [Required(ErrorMessage = "IssueId is required.")]
        public int IssueId { get; set; }

        /// <summary>
        /// User ID of the sender.
        /// </summary>
        [Required(ErrorMessage = "SenderId is required.")]
        public int SenderId { get; set; }

        /// <summary>
        /// User ID of the recipient.
        /// </summary>
        [Required(ErrorMessage = "RecipientId is required.")]
        public int RecipientId { get; set; }

        /// <summary>
        /// Subject of the message.
        /// </summary>
        [Required(ErrorMessage = "Subject is required.")]
        [StringLength(200, ErrorMessage = "Subject cannot exceed 200 characters.")]
        public string Subject { get; set; } = string.Empty;

        /// <summary>
        /// Body content of the message.
        /// </summary>
        [Required(ErrorMessage = "Body is required.")]
        [StringLength(2000, ErrorMessage = "Body cannot exceed 2000 characters.")]
        public string Body { get; set; } = string.Empty;

        /// <summary>
        /// Date and time when the message was sent.
        /// </summary>
        [Required(ErrorMessage = "SendDate is required.")]
        public DateTimeOffset SentAt { get; set; } = DateTimeOffset.UtcNow;
    }
}