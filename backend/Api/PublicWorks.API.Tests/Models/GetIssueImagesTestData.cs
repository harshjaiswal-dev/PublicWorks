namespace PublicWorks.API.Tests.Models
{
    public class GetIssueImagesTestData
    {
         public string? Scenario { get; set; }
        public int IssueId { get; set; }
        public int? ImageId { get; set; }
        public string? ImagePath { get; set; }
        public DateTime? UploadedAt { get; set; }
    }
}
