using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Model
{
    public class AuditTrail
    {
        [Key]
        public int Id { get; set; }  // PK

        [Required]
        public int? UserId { get; set; }  // FK to Users

        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }

        [Required]
        public string EntityName { get; set; } = string.Empty;

        [Required]
        public int RecordId { get; set; }  // Record in target table

        [Required]
        public int ActionTypeId { get; set; }  // FK to ActionTypes

        [ForeignKey(nameof(ActionTypeId))]
        public ActionType? ActionType { get; set; }

        public string OldValues { get; set; } = string.Empty;
        public string NewValues { get; set; } = string.Empty;

        [Required]
        public DateTimeOffset ActionDate { get; set; } = DateTimeOffset.UtcNow;

    }
}