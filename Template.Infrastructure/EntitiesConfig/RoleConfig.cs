using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities;

namespace Template.Infrastructure.EntitiesConfig;

public class RoleConfig : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(role => role.Id);

        builder.ToTable("Role");

        builder.HasIndex(role => role.Name).IsUnique();

        builder.HasData(new Role { Id = 1, Name = "SuperAdministrator" });

        // Relationships
        builder
       .HasMany(r => r.Permissions)
       .WithMany(p => p.Roles)
       .UsingEntity<Dictionary<string, object>>(
           "RolePermission",
           x => x
               .HasOne<Permission>()
               .WithMany()
               .HasForeignKey("PermissionId"),
           y => y
               .HasOne<Role>()
               .WithMany()
               .HasForeignKey("RoleId")
               ,
            z =>
            {
                var rolePermissions = Enum.GetValues(typeof(Domain.Enums.RightPermission))
                 .Cast<Domain.Enums.RightPermission>()
                 .Select(e => new { RoleId = 1, PermissionId = (int)e })
                 .ToArray();

                z.HasData(rolePermissions);
            });
    }
}
