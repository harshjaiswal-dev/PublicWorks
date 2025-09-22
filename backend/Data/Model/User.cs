using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Model
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }  // PK ID

        [MaxLength(100)]
        public string? GoogleUserId { get; set; } // Nullable, since not all users may log in with Google

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? PasswordHash { get; set; }

        [MaxLength(255)]
        public string? ProfilePicture { get; set; }  

        [Required]
        public int RoleId { get; set; }  // FK RoleID

        [ForeignKey(nameof(RoleId))]
        public Role? Role { get; set; }
              
        public DateTimeOffset? LastLoginAt { get; set; }  

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        public bool IsActive { get; set; } = true;
    }
}