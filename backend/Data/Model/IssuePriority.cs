using System.ComponentModel.DataAnnotations;

namespace Data.Model
{
    public class Priority
    {
        [Key]
        public int PriorityId { get; set; }  // PK ID

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Description { get; set; }

    }
}