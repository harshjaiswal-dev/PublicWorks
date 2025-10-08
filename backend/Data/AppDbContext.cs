using Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace Data
{
    public class AppDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private int _userId;

        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
            SetUserIdFromContext();
        }

        private void SetUserIdFromContext()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null) return;

            var user = httpContext.User;
            if (user == null) return;

            var claim = user.FindFirst("UserId");
            if (claim != null && int.TryParse(claim.Value, out int parsedUserId))
            {
                _userId = parsedUserId;
            }
            else
            {
                  int? _userId = null; 

            }
        }

        public DbSet<ActionType> ActionType { get; set; }
        public DbSet<AuditTrail> AuditTrail { get; set; }
        public DbSet<IssueCategory> IssueCategory { get; set; }
        public DbSet<IssueImage> IssueImage { get; set; }
        public DbSet<Issue> Issue { get; set; }
        public DbSet<IssueMessage> IssueMessage { get; set; }
        public DbSet<IssuePriority> IssuePriority { get; set; }
        public DbSet<IssueRemark> IssueRemark { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<IssueStatus> IssueStatus { get; set; }
        public DbSet<User> User { get; set; }

        private class TempAuditEntry
        {
            public EntityEntry Entry { get; set; } = default!;
            public string EntityName { get; set; } = string.Empty;
            public Dictionary<string, object?>? OldValues { get; set; }
            public Dictionary<string, object?>? NewValues { get; set; }
            public int ActionTypeId { get; set; }
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var auditEntries = OnBeforeSaveChanges();//gathers info about changes using change tracken and stores in tempauditentrylist
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            //EF saves all changes and updates the negative temp IDs with real DB values
            OnAfterSaveChanges(auditEntries);
            return result;
        }

        private List<TempAuditEntry> OnBeforeSaveChanges()
        {
            ChangeTracker.DetectChanges(); //ef is tracking the latest chnges

            var auditEntries = new List<TempAuditEntry>();//Creates a list to store all temporary audit entries.

            foreach (var entry in ChangeTracker.Entries())
            {//skips enteries that are of audittrail type and detached, and unchnaged
                if (entry.Entity is AuditTrail || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                var auditEntry = new TempAuditEntry
                {
                    Entry = entry,
                    EntityName = entry.Entity.GetType().Name
                };

                switch (entry.State)
                {
                    case EntityState.Added:
                        auditEntry.ActionTypeId = 1;
                        auditEntry.NewValues = entry.CurrentValues.Properties
                            .ToDictionary(p => p.Name, p => entry.CurrentValues[p]);
                        break;

                    case EntityState.Deleted:
                        auditEntry.ActionTypeId = 3;
                        auditEntry.OldValues = entry.OriginalValues.Properties
                            .ToDictionary(p => p.Name, p => entry.OriginalValues[p]);
                        break;

                    case EntityState.Modified:
                        auditEntry.ActionTypeId = 2;
                        auditEntry.OldValues = new Dictionary<string, object?>();
                        auditEntry.NewValues = new Dictionary<string, object?>();

                        foreach (var prop in entry.Properties)
                        {
                            if (!prop.IsModified)
                                continue;

                            var original = prop.OriginalValue?.ToString();
                            var current = prop.CurrentValue?.ToString();

                            if (original != current)
                            {
                                auditEntry.OldValues[prop.Metadata.Name] = prop.OriginalValue;
                                auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
                            }
                        }

                        if (!auditEntry.OldValues.Any() && !auditEntry.NewValues.Any())
                            continue;

                        break;
                }

                auditEntries.Add(auditEntry);
            }

            return auditEntries;
        }

        private void OnAfterSaveChanges(List<TempAuditEntry> tempEntries)
        {
            // ✅ Custom JSON settings to handle circular references and Geometry
            var jsonSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Converters = new List<JsonConverter> { new GeometryJsonConverter() }
            };
            foreach (var temp in tempEntries)
            {
                var entry = temp.Entry;

                var key = entry.Metadata.FindPrimaryKey();
                if (key == null || key.Properties.Count != 1)
                    continue;

                var pkProperty = key.Properties.First();
                var pkValue = entry.Property(pkProperty.Name).CurrentValue;

                int userId;

                if (pkValue == null || !int.TryParse(pkValue.ToString(), out int recordId) || recordId <= 0)
                    continue; // Only log if we have a valid, positive RecordId

                // Inject real primary key into NewValues (especially useful for 'Added' entries)
                if (temp.NewValues != null && temp.NewValues.ContainsKey(pkProperty.Name))
                {
                    temp.NewValues[pkProperty.Name] = recordId;
                }

                if (entry.Entity is User)
                    userId = Convert.ToInt32(pkValue);
                else
                {
                    // ✅ For other entities, get from claims
                    userId = _userId;
                }

                var audit = new AuditTrail
                {
                    UserId = userId,
                    EntityName = temp.EntityName,
                    ActionTypeId = temp.ActionTypeId,
                    ActionDate = DateTimeOffset.UtcNow,
                    RecordId = recordId,
                    OldValues = temp.OldValues != null ? JsonConvert.SerializeObject(temp.OldValues, jsonSettings) : string.Empty,
                    NewValues = temp.NewValues != null ? JsonConvert.SerializeObject(temp.NewValues, jsonSettings) : string.Empty
                };

                AuditTrail.Add(audit);
            }

            // Save audit logs in same transaction
            base.SaveChanges();
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IssueMessage>()
                .HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.SentByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<IssueMessage>()
                .HasOne(m => m.Receiver)
                .WithMany()
                .HasForeignKey(m => m.SentToUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<IssueMessage>()
                .HasOne(m => m.Issue)
                .WithMany()
                .HasForeignKey(m => m.IssueId)
                .OnDelete(DeleteBehavior.Cascade); // this one is safe
        }

    }
}
