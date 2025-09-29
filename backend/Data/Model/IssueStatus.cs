using System.ComponentModel.DataAnnotations;

namespace Data.Model
{
    public class Status
    {
        [Key]
        public int StatusId { get; set; }  // PK ID

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Description { get; set; }
    }
}