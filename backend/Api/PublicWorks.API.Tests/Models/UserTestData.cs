using System;

namespace PublicWorks.API.Tests.Models
{
    public class UserTestData
    {
      
       public int UserId { get; set; }
        public string? GoogleUserId { get; set; }
        public string? Name { get; set; }
        public string? PasswordHash { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ProfilePicture { get; set; }
        public int RoleId { get; set; }
        public DateTime? LastLoginAt { get; set; }    // nullable DateTime
        public DateTime? CreatedAt { get; set; }      // nullable DateTime
        public bool IsActive { get; set; }
        public string? Email { get; set; }

        // Claims used in GetProfile test
        public string? ClaimUserId { get; set; }
        public string? ClaimName { get; set; }
    }
}
