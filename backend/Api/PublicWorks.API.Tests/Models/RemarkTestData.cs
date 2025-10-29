// File: Tests/PublicWorks.API.Tests/Models/RemarkTestData.cs
namespace PublicWorks.API.Tests.Models
{
    public class RemarkTestData
    {
        public int RemarkId { get; set; }
        public int IssueId { get; set; }
        public string RemarkText { get; set; } = string.Empty;
        public int ExpectedStatus { get; set; }
    }
}
