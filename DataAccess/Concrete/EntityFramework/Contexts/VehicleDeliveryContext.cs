using Core.Entities;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework.Contexts;

public class VehicleDeliveryContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL("Server=localhost; Database=vehicleDelivery; Uid=root; Pwd=myStrongPassword");
    }

    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Owner> Owners { get; set; }
    public DbSet<Department> Departments { get; set; }

    public override int SaveChanges()
    {
        ChangeTracker.DetectChanges();
        var added = ChangeTracker.Entries()
            .Where(t => t.State == EntityState.Added)
            .Select(t => t.Entity)
            .ToArray();
        
        foreach (var entity in added)
        {
            if (entity is BaseEntity)
            {
                var track = entity as BaseEntity;
                track.Created = DateTime.Now;
                track.IsDeleted = false;
            }
        }
        
        var modified = this.ChangeTracker.Entries()
            .Where(t => t.State == EntityState.Modified)
            .Select(t => t.Entity)
            .ToArray();

        foreach (var entity in modified)
        {
            if (entity is BaseEntity)
            {
                var track = entity as BaseEntity;
                track.Changed = DateTime.Now;
            }
        }

        var deleted = ChangeTracker.Entries()
            .Where(t => t.State == EntityState.Deleted)
            .Select(t => t.Entity).ToArray();
        
        foreach (var entity in deleted)
        {
            if (entity is BaseEntity)
            {
                var track = entity as BaseEntity;
                track.IsDeleted = true;
            }
        }
        
        return base.SaveChanges();
    }
}