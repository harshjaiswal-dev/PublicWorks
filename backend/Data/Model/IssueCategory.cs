using System.ComponentModel.DataAnnotations;

namespace Data.Model;

public class Category
{
    [Key]
    public int CategoryId { get; set; }
 
    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;
 
    [MaxLength(500)]
    public string? Description { get; set; }
 
}