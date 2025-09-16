using System.ComponentModel.DataAnnotations;

namespace Data.Model
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }  // PK ID

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Description { get; set; }
    }
}