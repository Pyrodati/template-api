using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Template.Domain.Entities;

namespace Template.Infrastructure.EntitiesConfig;

public class PermissionConfig : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.HasKey(perm => perm.Id);

        builder.ToTable("Permission");

        builder.Property(perm => perm.Id).ValueGeneratedNever();
        builder.HasIndex(perm => perm.Name).IsUnique();

        builder.HasData(new Permission
        {
            Id = (int)Domain.Enums.RightPermission.USER_READ,
            Name = Domain.Enums.RightPermission.USER_READ.ToString()
        });

        builder.HasData(new Permission
        {
            Id = (int)Domain.Enums.RightPermission.USER_CREATE,
            Name = Domain.Enums.RightPermission.USER_CREATE.ToString()
        });
        builder.HasData(new Permission
        {
            Id = (int)Domain.Enums.RightPermission.USER_UPDATE,
            Name = Domain.Enums.RightPermission.USER_UPDATE.ToString()
        });
        builder.HasData(new Permission
        {
            Id = (int)Domain.Enums.RightPermission.USER_DELETE,
            Name = Domain.Enums.RightPermission.USER_DELETE.ToString()
        });
        builder.HasData(new Permission
        {
            Id = (int)Domain.Enums.RightPermission.USER_UPDATE_PASSWORD,
            Name = Domain.Enums.RightPermission.USER_UPDATE_PASSWORD.ToString()
        });
        builder.HasData(new Permission
        {
            Id = (int)Domain.Enums.RightPermission.ROLE_READ,
            Name = Domain.Enums.RightPermission.ROLE_READ.ToString()
        });
        builder.HasData(new Permission
        {
            Id = (int)Domain.Enums.RightPermission.ROLE_CREATE,
            Name = Domain.Enums.RightPermission.ROLE_CREATE.ToString()
        });
        builder.HasData(new Permission
        {
            Id = (int)Domain.Enums.RightPermission.ROLE_UPDATE,
            Name = Domain.Enums.RightPermission.ROLE_UPDATE.ToString()
        });
        builder.HasData(new Permission
        {
            Id = (int)Domain.Enums.RightPermission.ROLE_DELETE,
            Name = Domain.Enums.RightPermission.ROLE_DELETE.ToString()
        });
    }
}
