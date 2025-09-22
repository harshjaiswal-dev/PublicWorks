namespace Business.DTOs
{
    public class IssueDto
    {
        public int IssueId { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public decimal Lat { get; set; }
        public decimal Long { get; set; }
        public string Description { get; set; } = string.Empty;
        public int PriorityId { get; set; }
        public int StatusId { get; set; }
    }
}