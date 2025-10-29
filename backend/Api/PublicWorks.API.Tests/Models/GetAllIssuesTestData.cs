namespace PublicWorks.API.Tests.Models
{
    public class GetAllIssuesTestData
    {
        public string Scenario { get; set; } = string.Empty; // "Single", "Multiple", "Empty"
    public int? Id { get; set; }
    public int? StatusId { get; set; }
    public int? PriorityId { get; set; }
    public int? CategoryId { get; set; }
    public string Description { get; set; } = string.Empty;
    }
}