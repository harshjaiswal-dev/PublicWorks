using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Model
{
    public class Message
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int IssueId { get; set; }

        [ForeignKey(nameof(IssueId))]
        public Issue Issue { get; set; } = null!;
    
        [Required]
        public int SendBy { get; set; }
    
        [Required]
        public int SendTo { get; set; }      

        [Required, MaxLength(255)]
        public string Subject { get; set; } = string.Empty;

        [Required, MaxLength(500)]
        public string Body { get; set; } = string.Empty;
    
        public DateTime SendDate { get; set; }
    }
}