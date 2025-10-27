namespace PublicWorks.API.Tests.Models
{
    public class StatusCsvData
    {
        public string Scenario { get; set; } = string.Empty;
        public int Id { get; set; }
        public string? ExpectedStatusesJson { get; set; }
        public string? ExpectedStatusJson { get; set; }
    }
}
