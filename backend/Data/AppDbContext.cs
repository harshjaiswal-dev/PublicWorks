using Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ActionType> ActionType { get; set; }
        public DbSet<AuditTrail> AuditTrail { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Issue> Issue { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<Priority> Priority { get; set; }
        public DbSet<Remark> Remark { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<User> User { get; set; }
        
    }
}
