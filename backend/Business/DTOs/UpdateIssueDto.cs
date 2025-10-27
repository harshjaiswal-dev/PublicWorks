using System.ComponentModel.DataAnnotations;

namespace Business.DTOs
{
    public class UpdateIssueDto
    {
        public int StatusId { get; set; }
        public int PriorityId { get; set; }
        public int CategoryId { get; set; }
    }

}