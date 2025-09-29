using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Model
{
    public class Image
    {
        [Key]
        public int ID { get; set; }
    
        [Required]
        public int IssueId { get; set; }

        [ForeignKey(nameof(IssueId))]
        public Issue? Issue { get; set; }

        [Required, MaxLength(255)]
        public string ImagePath { get; set; } = string.Empty;
    
        public DateTimeOffset UploadedAt { get; set; }
    }  
}