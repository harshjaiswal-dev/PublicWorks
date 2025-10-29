namespace PublicWorks.API.Tests.Models
{
    public class MessageCsvData
    {
        public string Scenario { get; set; } = string.Empty;
        public int Id { get; set; }
        public string? ExpectedMessagesJson { get; set; }
        public string? ExpectedMessageJson { get; set; }
    }
}
