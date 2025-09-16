using Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Priority> Priority { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<User> User { get; set; }
        
    }
}
