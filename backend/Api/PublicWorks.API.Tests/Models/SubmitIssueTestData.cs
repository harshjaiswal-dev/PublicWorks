namespace PublicWorks.API.Tests.Models
{
    public class SubmitIssueTestData
    {
         public int UserId { get; set; }
    public int CategoryId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Description { get; set; } = string.Empty;
    public int ExpectedIssueId { get; set; }
    public bool ExpectedSuccess { get; set; }
    }
}