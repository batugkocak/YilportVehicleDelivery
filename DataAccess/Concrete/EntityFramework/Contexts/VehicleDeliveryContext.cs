using Core.Entities;
using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Task = Entities.Concrete.Task;

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
    public DbSet<Driver> Drivers { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<VehicleOnTask> VehiclesOnTask { get; set; }
    
    //Auth:
    public DbSet<OperationClaim> OperationClaims { get; set; }
    
    public DbSet<User> Users { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

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
                if (track != null)
                {
                    track.Created = DateTime.Now;
                    track.IsDeleted = false;
                }
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
                if (track != null) track.Changed = DateTime.Now;
            }
        }
        return base.SaveChanges();
    }
}