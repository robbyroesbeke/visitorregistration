// (c) Visitor Registration

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using VisitorRegistration.BE.Core.Common;
using VisitorRegistration.BE.Core.Entities;

namespace VisitorRegistration.BE.Infrastructure.Persistence.EF;

public class VisitorRegistrationDbContext : DbContext
{
    public VisitorRegistrationDbContext (
        DbContextOptions<VisitorRegistrationDbContext> options) : base(options)
    {
        _ = Database.EnsureCreated();
    }

    public DbSet<Company> Companies { get; set; } = default!;

    public DbSet<Employee> Employees { get; set; } = default!;

    protected override void OnModelCreating (ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        _ = modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(VisitorRegistrationDbContext).Assembly);
    }

    public override Task<int> SaveChangesAsync (CancellationToken cancellationToken = default)
    {
        foreach ( EntityEntry<AudibleEntity> entry in ChangeTracker.Entries<AudibleEntity>() )
        {
            if ( entry.State == EntityState.Added )
            {
                entry.Entity.CreatedOn = DateTime.UtcNow;
            }

            if ( entry.State == EntityState.Modified )
            {
                entry.Entity.LastModifiedOn = DateTime.UtcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}