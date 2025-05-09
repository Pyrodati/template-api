﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Template.Infrastructure;

#nullable disable

namespace Template.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250409155834_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RolePermission", b =>
                {
                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("PermissionId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("RolePermission");

                    b.HasData(
                        new
                        {
                            PermissionId = 100,
                            RoleId = 1
                        },
                        new
                        {
                            PermissionId = 101,
                            RoleId = 1
                        },
                        new
                        {
                            PermissionId = 102,
                            RoleId = 1
                        },
                        new
                        {
                            PermissionId = 103,
                            RoleId = 1
                        },
                        new
                        {
                            PermissionId = 104,
                            RoleId = 1
                        },
                        new
                        {
                            PermissionId = 110,
                            RoleId = 1
                        },
                        new
                        {
                            PermissionId = 111,
                            RoleId = 1
                        },
                        new
                        {
                            PermissionId = 112,
                            RoleId = 1
                        },
                        new
                        {
                            PermissionId = 113,
                            RoleId = 1
                        });
                });

            modelBuilder.Entity("Template.Domain.Entities.Permission", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Permission", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 100,
                            Name = "USER_READ"
                        },
                        new
                        {
                            Id = 101,
                            Name = "USER_CREATE"
                        },
                        new
                        {
                            Id = 102,
                            Name = "USER_UPDATE"
                        },
                        new
                        {
                            Id = 103,
                            Name = "USER_DELETE"
                        },
                        new
                        {
                            Id = 104,
                            Name = "USER_UPDATE_PASSWORD"
                        },
                        new
                        {
                            Id = 110,
                            Name = "ROLE_READ"
                        },
                        new
                        {
                            Id = 111,
                            Name = "ROLE_CREATE"
                        },
                        new
                        {
                            Id = 112,
                            Name = "ROLE_UPDATE"
                        },
                        new
                        {
                            Id = 113,
                            Name = "ROLE_DELETE"
                        });
                });

            modelBuilder.Entity("Template.Domain.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Role", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "SuperAdministrator"
                        });
                });

            modelBuilder.Entity("Template.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("User", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "sa",
                            FirstName = "Super",
                            IsActive = true,
                            LastName = "Admin",
                            Password = "ACxfS5Nm92bkZiWj7KcgsEZPVAOIZAnvbtwvsZdRg+9PfrOuMYpcNTBWIokrNacdrw=="
                        });
                });

            modelBuilder.Entity("UserRole", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("RoleId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole");

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("RolePermission", b =>
                {
                    b.HasOne("Template.Domain.Entities.Permission", null)
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Template.Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UserRole", b =>
                {
                    b.HasOne("Template.Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Template.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
