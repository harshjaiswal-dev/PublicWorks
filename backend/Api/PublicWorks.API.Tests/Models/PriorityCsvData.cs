namespace PublicWorks.API.Tests.Models
{
    public class PriorityCsvData
    {
        public string Scenario { get; set; } = string.Empty;
        public int Id { get; set; }
        public string? ExpectedPrioritiesJson { get; set; }
        public string? ExpectedPriorityJson { get; set; }
    }
}
