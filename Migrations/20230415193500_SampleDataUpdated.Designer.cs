﻿// <auto-generated />
using System;
using DeliverySoftware.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DeliverySoftware.Migrations
{
    [DbContext(typeof(DeliveryDBContext))]
    [Migration("20230415193500_SampleDataUpdated")]
    partial class SampleDataUpdated
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DeliverySoftware.Business.Delivery.Delivery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("NumberOfPackages")
                        .HasColumnType("int");

                    b.Property<Guid>("UID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("VanUID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Deliveries");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            NumberOfPackages = 1,
                            UID = new Guid("3f3cf1cd-131d-4d05-93b9-21dbd082fcac"),
                            VanUID = new Guid("f2b87d27-d4d8-425e-8df8-b3e7bf7f6460")
                        });
                });

            modelBuilder.Entity("DeliverySoftware.Business.Delivery.Package", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("CustomerUID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DeliveryUID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAssignedToDelivery")
                        .HasColumnType("bit");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.Property<Guid>("UID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Packages");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CustomerUID = new Guid("751e25c8-68ba-49dc-b1de-15fcb0bf210f"),
                            DeliveryUID = new Guid("3f3cf1cd-131d-4d05-93b9-21dbd082fcac"),
                            Description = "Big box of bolts",
                            IsAssignedToDelivery = true,
                            Size = 10,
                            UID = new Guid("721a28e5-03b7-4c9a-863a-53d58e23d64a")
                        });
                });

            modelBuilder.Entity("DeliverySoftware.Business.Fleet.Van", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<Guid?>("DriverUID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Registration")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Vans");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Capacity = 50,
                            DriverUID = new Guid("16776aef-a5ff-424d-be7e-ea3fd591ce90"),
                            Registration = "WK04 DHC",
                            UID = new Guid("f2b87d27-d4d8-425e-8df8-b3e7bf7f6460")
                        });
                });

            modelBuilder.Entity("DeliverySoftware.Business.Users.DeliveryUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Forename")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("HouseNumber")
                        .HasColumnType("int");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("PostCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "751e25c8-68ba-49dc-b1de-15fcb0bf210f",
                            AccessFailedCount = 0,
                            Address = "306 Sinfin Avenue, Derby, UK",
                            ConcurrencyStamp = "3fe53337-68fc-4ebf-9c2a-70b3e5e84d34",
                            Email = "petelampy@gmail.com",
                            EmailConfirmed = true,
                            Forename = "Annika",
                            HouseNumber = 306,
                            LockoutEnabled = false,
                            NormalizedEmail = "PETELAMPY@GMAIL.COM",
                            NormalizedUserName = "CUSTOMERLOGON",
                            PasswordHash = "AQAAAAIAAYagAAAAEGkC1RW3x6M4loVLP5X7504Sr4DhUV3lJ6YCS6KQVFm3uPzI6rQFrGSnncEGp+j8CQ==",
                            PhoneNumberConfirmed = false,
                            PostCode = "DE24 9QX",
                            SecurityStamp = "06f19558-fe00-48e1-bfa3-61655bd9ff5a",
                            Surname = "Hansen",
                            TwoFactorEnabled = false,
                            UserName = "customerlogon",
                            UserType = 1
                        },
                        new
                        {
                            Id = "16776aef-a5ff-424d-be7e-ea3fd591ce90",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "f8130406-5e90-459b-b4d2-564e2baf29ab",
                            Email = "petelampy@gmail.com",
                            EmailConfirmed = true,
                            Forename = "Din",
                            LockoutEnabled = false,
                            NormalizedEmail = "PETELAMPY@GMAIL.COM",
                            NormalizedUserName = "DRIVERLOGON",
                            PasswordHash = "AQAAAAIAAYagAAAAEISrd1O2BFKeF8/h+LRnYQhMAmBI+Vm6TtA0sHaScwdpPLSdakCE037ngA35TKs0qQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "59672046-6d5c-42bd-b99f-962b248d662e",
                            Surname = "Djarin",
                            TwoFactorEnabled = false,
                            UserName = "driverlogon",
                            UserType = 2
                        },
                        new
                        {
                            Id = "ca6f5584-527f-4820-802f-bd329352c3e5",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "af702541-4939-4bb7-9895-e246c0ec292f",
                            Email = "petelampy@gmail.com",
                            EmailConfirmed = true,
                            Forename = "William",
                            LockoutEnabled = false,
                            NormalizedEmail = "PETELAMPY@GMAIL.COM",
                            NormalizedUserName = "EMPLOYEELOGON",
                            PasswordHash = "AQAAAAIAAYagAAAAEMHaH1GpZL+Yvzax1VDnJkj5QnUIBBFSLEQaMHputEYiafWop6f0T4RQALyxIri+uQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "7bb79ccf-8cf0-46c0-9fa1-31074f58482f",
                            Surname = "Riker",
                            TwoFactorEnabled = false,
                            UserName = "employeelogon",
                            UserType = 3
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("DeliverySoftware.Business.Users.DeliveryUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("DeliverySoftware.Business.Users.DeliveryUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DeliverySoftware.Business.Users.DeliveryUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("DeliverySoftware.Business.Users.DeliveryUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
