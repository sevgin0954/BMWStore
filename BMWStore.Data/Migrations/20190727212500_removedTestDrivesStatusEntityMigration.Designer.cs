﻿// <auto-generated />
using System;
using BMWStore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BMWStore.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190727212500_removedTestDrivesStatusEntityMigration")]
    partial class removedTestDrivesStatusEntityMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BMWStore.Entities.BaseCar", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Acceleration_0_100Km");

                    b.Property<int>("CO2Emissions");

                    b.Property<string>("ColorName")
                        .IsRequired();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<double>("Displacement");

                    b.Property<int>("DoorsCount");

                    b.Property<string>("EngineId")
                        .IsRequired();

                    b.Property<double>("FuelConsumation_City_Litres_100Km");

                    b.Property<double>("FuelConsumation_Highway_Litres_100Km");

                    b.Property<string>("FuelTypeId")
                        .IsRequired();

                    b.Property<double>("HoursePower");

                    b.Property<string>("ModelTypeId")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<decimal>("Price");

                    b.Property<string>("SeriesId")
                        .IsRequired();

                    b.Property<decimal>("Torque");

                    b.Property<string>("Vin")
                        .IsRequired()
                        .HasMaxLength(17);

                    b.Property<int>("WarrantyMonthsLeft");

                    b.Property<int>("Weight_Kg");

                    b.Property<string>("Year");

                    b.HasKey("Id");

                    b.HasIndex("EngineId");

                    b.HasIndex("FuelTypeId");

                    b.HasIndex("ModelTypeId");

                    b.HasIndex("SeriesId");

                    b.ToTable("BaseCars");

                    b.HasDiscriminator<string>("Discriminator").HasValue("BaseCar");
                });

            modelBuilder.Entity("BMWStore.Entities.CarOption", b =>
                {
                    b.Property<string>("CarId");

                    b.Property<string>("OptionId");

                    b.HasKey("CarId", "OptionId");

                    b.HasIndex("OptionId");

                    b.ToTable("CarsOptions");
                });

            modelBuilder.Entity("BMWStore.Entities.Engine", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<decimal>("Price");

                    b.Property<string>("TransmissionId")
                        .IsRequired();

                    b.Property<int>("Weight_Kg");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("TransmissionId");

                    b.ToTable("Engines");
                });

            modelBuilder.Entity("BMWStore.Entities.FuelType", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("FuelTypes");
                });

            modelBuilder.Entity("BMWStore.Entities.ModelType", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("ModelTypes");
                });

            modelBuilder.Entity("BMWStore.Entities.Option", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<decimal>("Price");

                    b.HasKey("Id");

                    b.ToTable("Options");
                });

            modelBuilder.Entity("BMWStore.Entities.Picture", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CarId")
                        .IsRequired();

                    b.Property<string>("PublicId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.ToTable("Pictures");
                });

            modelBuilder.Entity("BMWStore.Entities.Series", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Series");
                });

            modelBuilder.Entity("BMWStore.Entities.Status", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.HasKey("Id");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("BMWStore.Entities.TestDrive", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CarId")
                        .IsRequired();

                    b.Property<string>("Comment")
                        .HasMaxLength(300);

                    b.Property<DateTime>("ScheduleDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(new DateTime(2019, 7, 27, 21, 24, 59, 177, DateTimeKind.Utc).AddTicks(6578));

                    b.Property<string>("StatusId")
                        .IsRequired();

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("StatusId");

                    b.HasIndex("UserId");

                    b.ToTable("TestDrives");
                });

            modelBuilder.Entity("BMWStore.Entities.Transmission", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<decimal>("Price");

                    b.HasKey("Id");

                    b.ToTable("Transmissions");
                });

            modelBuilder.Entity("BMWStore.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("BMWStore.Entities.NewCar", b =>
                {
                    b.HasBaseType("BMWStore.Entities.BaseCar");

                    b.HasDiscriminator().HasValue("NewCar");
                });

            modelBuilder.Entity("BMWStore.Entities.UsedCar", b =>
                {
                    b.HasBaseType("BMWStore.Entities.BaseCar");

                    b.Property<double>("Mileage");

                    b.HasDiscriminator().HasValue("UsedCar");
                });

            modelBuilder.Entity("BMWStore.Entities.BaseCar", b =>
                {
                    b.HasOne("BMWStore.Entities.Engine", "Engine")
                        .WithMany("Cars")
                        .HasForeignKey("EngineId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BMWStore.Entities.FuelType", "FuelType")
                        .WithMany("Cars")
                        .HasForeignKey("FuelTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BMWStore.Entities.ModelType", "ModelType")
                        .WithMany("Cars")
                        .HasForeignKey("ModelTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BMWStore.Entities.Series", "Series")
                        .WithMany("Cars")
                        .HasForeignKey("SeriesId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BMWStore.Entities.CarOption", b =>
                {
                    b.HasOne("BMWStore.Entities.BaseCar", "Car")
                        .WithMany("Options")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BMWStore.Entities.Option", "Option")
                        .WithMany("Cars")
                        .HasForeignKey("OptionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BMWStore.Entities.Engine", b =>
                {
                    b.HasOne("BMWStore.Entities.Transmission", "Transmission")
                        .WithMany("Engines")
                        .HasForeignKey("TransmissionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BMWStore.Entities.Picture", b =>
                {
                    b.HasOne("BMWStore.Entities.BaseCar", "Car")
                        .WithMany("Pictures")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BMWStore.Entities.TestDrive", b =>
                {
                    b.HasOne("BMWStore.Entities.BaseCar", "Car")
                        .WithMany("TestDrives")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BMWStore.Entities.Status", "Status")
                        .WithMany("TestDrives")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BMWStore.Entities.User", "User")
                        .WithMany("TestDrives")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("BMWStore.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("BMWStore.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BMWStore.Entities.User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("BMWStore.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
