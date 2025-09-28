using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite.Geometries;


namespace Data.Model
{
    public class Issue
    {
        [Key]
        public int IssueId { get; set; }

        [Required(ErrorMessage = "User is required.")]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }
        
        [Required(ErrorMessage = "Category is required.")]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category? Category { get; set; }

        // [Required(ErrorMessage = "Latitude is required.")]
        // [Column(TypeName = "decimal(9,6)")]
        // [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90.")]
        // public decimal Lat { get; set; }

        // [Required(ErrorMessage = "Longitude is required.")]
        // [Column(TypeName = "decimal(9,6)")]
        // [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180.")]
        // public decimal Long { get; set; }
         [Required]
        [Column(TypeName = "geography")] // Important
        public Point Location { get; set; } = null!;
        
    public string LocationText { get; set; }  // new property to store WKT text


        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string Description { get; set; } = string.Empty;
    
        [Required(ErrorMessage = "Priority is required.")]
        public int PriorityId { get; set; }

        [ForeignKey(nameof(PriorityId))]
        public Priority? Priority { get; set; }
    
        [Required(ErrorMessage = "Status is required.")]
        public int StatusId { get; set; }
    
        [ForeignKey(nameof(StatusId))]
        public Status? Status { get; set; } 
    
    }
}