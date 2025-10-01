namespace Business.DTOs
{
    public class IssueSummaryDto
    {
        public int TotalIssues { get; set; }
        public int Pending { get; set; }
        public int InProgress { get; set; }
        public int Resolved { get; set; }
    }
}