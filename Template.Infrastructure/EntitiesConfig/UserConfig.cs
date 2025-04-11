using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities;

namespace Template.Infrastructure.EntitiesConfig;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);

        builder.ToTable("User");

        builder.HasIndex(user => user.Email).IsUnique();

        builder.Property(user => user.Id).ValueGeneratedOnAdd();

        builder.Navigation(user => user.Roles).AutoInclude();

        builder
            .HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity<Dictionary<string, object>>(
                "UserRole",
                x => x
                    .HasOne<Role>()
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Cascade),
                y => y
                    .HasOne<User>()
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade),
               z =>
               {
                   z.HasData(new { UserId = 1, RoleId = 1 });
               }); ;

        builder.HasData(new User
        {
            Id = 1,
            FirstName = "Super",
            LastName = "Admin",
            Email = "sa",
            Password = "ACxfS5Nm92bkZiWj7KcgsEZPVAOIZAnvbtwvsZdRg+9PfrOuMYpcNTBWIokrNacdrw==",
            IsActive = true
        });
    }
}
