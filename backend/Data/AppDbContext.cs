using System.Text.Json;
using Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace Data
{
    public class AppDbContext : DbContext
    {
        //IHttpContextAccessor is used to access the current HTTP request context
    private readonly IHttpContextAccessor _httpContextAccessor;
    private int? _userId;//current logged-in user's ID

        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
              SetUserIdFromContext();
        }
        //extract the currently logged-in user's ID from the HTTP request and store it in the _userId field.
//This ID is then used to track who made changes to the database
        private void SetUserIdFromContext()
        {

            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null) return;

            var user = httpContext.User;//current authenticated user.
            if (user == null) return;
//if present userId can be parsed to int and later used for userid.audittrail
            var claim = user.FindFirst("UserId");
            if (claim != null && int.TryParse(claim.Value, out int parsedUserId))
            {
                _userId = parsedUserId;
            }
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
        public DbSet<User> Users { get; set; }
        //This method is called whenever changes are saved to the database.
          public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            OnBeforeSaveChanges();
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

    private void OnBeforeSaveChanges()
    {
        ChangeTracker.DetectChanges();//detects all modifications made to entities.

        var auditEntries = new List<AuditTrail>();//collect the audit logs for each entity being changed.
        

        foreach (var entry in ChangeTracker.Entries())
        //gives you all entities currently being tracked by EF Core
            {
                // Skip AuditTrail entity itself and ignore Detached/Unchanged entities
                //detached is not tracked by efcore now and if no chnage  then nothing to audit
                if (entry.Entity is AuditTrail || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                var auditEntry = CreateAuditTrailEntry(entry);//analyzes this entity entry and generates an audit log
                if (auditEntry != null)
                {
                    auditEntries.Add(auditEntry);
                }
            }

        if (auditEntries.Any())
        {
            AuditTrail.AddRange(auditEntries);
        }
    }

    private AuditTrail? CreateAuditTrailEntry(EntityEntry entry)
    {
        var audit = new AuditTrail();

            // Set UserId if available, otherwise 0 or nullable
            audit.UserId = _userId;

        audit.EntityName = entry.Entity.GetType().Name;
        audit.ActionDate = DateTimeOffset.UtcNow;

        // Get primary key value (assumes single key)
        var key = entry.Metadata.FindPrimaryKey();
        if (key == null)
        {
            // No primary key defined, cannot audit
            return null;
        }

        // For simplicity, support single primary key only
        if (key.Properties.Count != 1)
        {
            // Handle composite keys if needed
            return null;
        }

        var pkProperty = key.Properties.First();
        var pkCurrentValue = entry.Property(pkProperty.Name).CurrentValue;

        // Try to convert PK to int (your AuditTrail expects int RecordId)
        if (pkCurrentValue == null || !int.TryParse(pkCurrentValue.ToString(), out int recordId))
        {
            // Cannot convert primary key to int, skip audit
            return null;
        }
        audit.RecordId = recordId;

        // Determine action type and map to your ActionType entity or IDs
        switch (entry.State)
        {
            case EntityState.Added:
                audit.ActionTypeId = 1; // Assuming 1 = Create in ActionType table
                audit.NewValues = SerializeProperties(entry.CurrentValues.Properties, entry.CurrentValues);
                audit.OldValues = string.Empty;
                break;

            case EntityState.Deleted:
                audit.ActionTypeId = 3; // Assuming 3 = Delete
                audit.OldValues = SerializeProperties(entry.OriginalValues.Properties, entry.OriginalValues);
                audit.NewValues = string.Empty;
                break;

            case EntityState.Modified:
                audit.ActionTypeId = 2; // Assuming 2 = Update
                 var changedProps = new List<string>();

    foreach (var prop in entry.Properties)
    {
        if (!prop.IsModified) continue;

        var original = prop.OriginalValue?.ToString();
        var current = prop.CurrentValue?.ToString();

        if (original != current) // strict comparison
        {
            changedProps.Add(prop.Metadata.Name);
        }
    }

    if (!changedProps.Any()) return null; // no real changes

    audit.OldValues = SerializeProperties(
        entry.OriginalValues.Properties.Where(p => changedProps.Contains(p.Name)), 
        entry.OriginalValues
    );

    audit.NewValues = SerializeProperties(
        entry.CurrentValues.Properties.Where(p => changedProps.Contains(p.Name)), 
        entry.CurrentValues
    );
    break;
            default:
                // Ignore other states
                return null;
        }

        return audit;
    }
//converting to json
    private string SerializeProperties(IEnumerable<Microsoft.EntityFrameworkCore.Metadata.IProperty> properties, PropertyValues values)
    {
        var dict = new Dictionary<string, object?>();//Maps each property name to its value
        foreach (var prop in properties)
        {
            var val = values[prop.Name];//gets actual valu 
            dict[prop.Name] = val;
        }

        return JsonConvert.SerializeObject(dict);//converts to json
    }
}


        
    }

