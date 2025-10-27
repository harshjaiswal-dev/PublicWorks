using System.ComponentModel.DataAnnotations;

namespace Business.DTOs
{
    public class SubmitIssueResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = null!;
        public int IssueId { get; set; }
    }
}
