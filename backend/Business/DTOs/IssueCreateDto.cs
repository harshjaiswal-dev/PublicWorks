using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using NetTopologySuite.Geometries;

namespace Business.DTOs
{
    /// <summary>
    /// DTO for creating a new issue.
    /// </summary>
    public class IssueCreateDto
    {
        /// <summary>
        /// Identifier of the user who created the issue.
        /// </summary>
        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { get; set; }

        /// <summary>
        /// Identifier of the category this issue belongs to.
        /// </summary>
        [Required(ErrorMessage = "CategoryId is required.")]
        public int CategoryId { get; set; }

        /// <summary>
        /// Latitude of the location.
        /// </summary>
        [Required(ErrorMessage = "Latitude is required.")]
        [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90.")]
        public double Latitude { get; set; }

        /// <summary>
        /// Longitude of the location.
        /// </summary>
        [Required(ErrorMessage = "Longitude is required.")]
        [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180.")]
        public double Longitude { get; set; }
        
        /// <summary>
        /// Detailed description of the issue.
        /// </summary>
        [Required(ErrorMessage = "Description is required.")]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 1000 characters.")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Uploaded image(s) related to the issue.
        /// </summary>
        [MinLength(1, ErrorMessage = "At least one image is required.")]
        public List<IFormFile> Images { get; set; } = new List<IFormFile>();
    }
}