namespace PublicWorks.API.Tests.Models
{
    public class GetIssueByIdTestData
    {
    public int Id { get; set; }
    public string Scenario { get; set; } = string.Empty; // "Valid", "NotFound", "BadRequest"
    public int StatusId { get; set; }
    public int PriorityId { get; set; }
    public int CategoryId { get; set; }
    public string Description { get; set; } = string.Empty;
    }
}


