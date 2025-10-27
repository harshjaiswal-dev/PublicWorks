namespace PublicWorks.API.Tests.Models
{
    public class UpdateIssueTestData
    {
         public int IssueId { get; set; }
    public int StatusId { get; set; }
    public int PriorityId { get; set; }
    public int CategoryId { get; set; }
    public bool Exists { get; set; }
    public bool ThrowsException { get; set; }
    public int ExpectedStatusCode { get; set; }
    public bool ExpectedSuccess { get; set; }
    public int ExpectedIssueId { get; set; }
    public int ExpectedStatusId { get; set; }
    public string ExpectedMessage { get; set; }
    }
}