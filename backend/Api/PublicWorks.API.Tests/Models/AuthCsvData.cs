namespace PublicWorks.API.Tests.Models
{
    public class AuthCsvData
    {
         public string? Scenario { get; set; }  // e.g., "ValidAdmin", "InvalidAdmin", "GoogleValid", "GoogleInvalid"
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Code { get; set; }      // For Google login scenarios
    public string? Role { get; set; }      // Admin/User
    public int ExpectedStatus { get; set; } 
    }
}
