using System.ComponentModel.DataAnnotations;

namespace Data.Model
{
    public class ActionType
    {
        [Key]
        public int ActionTypeId { get; set; }

        [Required]
        [MaxLength(20)]
        public string ActionName { get; set; } = string.Empty;  // "INSERT", "UPDATE", "DELETE"

    }
}