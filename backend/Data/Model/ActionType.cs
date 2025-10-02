using System.ComponentModel.DataAnnotations;

namespace Data.Model
{
    /// <summary>
    /// Represents the type of action performed on an entity, 
    /// used for audit logging (e.g., INSERT, UPDATE, DELETE).
    /// </summary>
    public class ActionType
    {
        /// <summary>
        /// Primary key for the ActionType entity.
        /// </summary>
        [Key]
        public int ActionTypeId { get; set; }

        /// <summary>
        /// Name of the action performed.
        /// Examples: "INSERT", "UPDATE", "DELETE".
        /// </summary>
        [Required(ErrorMessage = "Action name is required.")]
        [MaxLength(20, ErrorMessage = "Action name cannot exceed 20 characters.")]
        public string ActionName { get; set; } = string.Empty;
    }
}