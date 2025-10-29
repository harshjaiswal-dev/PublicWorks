namespace PublicWorks.API.Tests.Models
{
    public class RoleTestData
    {
        public int RoleId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string Scenario { get; set; } = null!; // required for CSV
    }
}
