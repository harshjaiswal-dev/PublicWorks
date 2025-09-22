namespace Business.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; }  
        public string? GoogleUserId { get; set; } 
        public string Name { get; set; } = string.Empty;
        public string? PasswordHash { get; set; }
        public string? ProfilePicture { get; set; }  
        public int RoleId { get; set; }              
        public DateTimeOffset? LastLoginAt { get; set; }  
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public bool IsActive { get; set; } = true;
    }
}