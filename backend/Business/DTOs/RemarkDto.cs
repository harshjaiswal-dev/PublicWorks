namespace Business.DTOs
{
    public class RemarkDto
    {
        public int ID { get; set; }
        public int IssueId { get; set; }
        public string RemarkText { get; set; } = string.Empty;
        public int RemarkBy { get; set; } 
        public DateTimeOffset RemarkAt { get; set; }
    }
}