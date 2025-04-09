using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Template.Domain.Entities;

namespace Template.Infrastructure;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); //ApplyConfiguration for every implementation of IEntityTypeConfiguration 
        base.OnModelCreating(modelBuilder);
    }

    public virtual DbSet<Permission> Permission { get; set; }
    public virtual DbSet<Role> Role { get; set; }
    public virtual DbSet<User> User { get; set; }
}
