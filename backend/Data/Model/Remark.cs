using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Model
{
    public class Remark
    {
        [Key]
        public int ID { get; set; }
    
        [Required]
        public int IssueId { get; set; }

        [ForeignKey(nameof(IssueId))]
        public Issue? Issue { get; set; }

        [Required, MaxLength(1000)]
        public string RemarkText { get; set; } = string.Empty;
    
        [Required]
        public int RemarkBy { get; set; } 
    
        [Required]
        public DateTimeOffset RemarkAt { get; set; }
    }
}