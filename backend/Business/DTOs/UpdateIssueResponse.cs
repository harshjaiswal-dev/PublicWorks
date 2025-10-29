using System.ComponentModel.DataAnnotations;

namespace Business.DTOs
{
    public class UpdateIssueResponse
    {
        public int IssueId { get; set; }
        public int StatusId { get; set; }
        public string? Message { get; set; }
    }

}
