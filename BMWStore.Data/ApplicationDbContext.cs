using BMWStore.Common.Constants;
using BMWStore.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace BMWStore.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<BaseCar> BaseCars { get; set; }
        public DbSet<Engine> Engines { get; set; }
        public DbSet<FuelType> FuelTypes { get; set; }
        public DbSet<ModelType> ModelTypes { get; set; }
        public DbSet<NewCar> NewCars { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<Transmission> Transmissions { get; set; }
        public DbSet<UsedCar> UsedCars { get; set; }
        public new DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<BaseCar>(car =>
            {
                car.HasKey(c => c.Id);

                car.HasOne(c => c.Engine)
                    .WithMany(e => e.Cars)
                    .HasForeignKey(c => c.EngineId);

                car.HasOne(c => c.FuelType)
                    .WithMany(ft => ft.Cars)
                    .HasForeignKey(c => c.FuelTypeId);

                car.HasOne(c => c.ModelType)
                    .WithMany(mt => mt.Cars)
                    .HasForeignKey(c => c.ModelTypeId);

                car.HasOne(c => c.Series)
                    .WithMany(s => s.Cars)
                    .HasForeignKey(c => c.SeriesId);

                car.HasMany(c => c.Orders)
                    .WithOne(b => b.Car)
                    .HasForeignKey(uc => uc.CarId);
            });

            builder.Entity<Engine>(engine =>
            {
                engine.HasKey(e => e.Id);

                engine.HasIndex(e => e.Name)
                    .IsUnique();

                engine.Property(e => e.Name)
                    .HasMaxLength(EntitiesConstants.EngineeNameMaxLength);

                engine.HasOne(e => e.Transmission)
                    .WithMany(t => t.Engines)
                    .HasForeignKey(e => e.TransmissionId);
            });

            builder.Entity<FuelType>(fuelType =>
            {
                fuelType.HasKey(ft => ft.Id);

                fuelType.HasIndex(ft => ft.Name)
                    .IsUnique();

                fuelType.Property(ft => ft.Name)
                    .HasMaxLength(EntitiesConstants.FuelTypeNameMaxLength);
            });

            builder.Entity<ModelType>(modelType =>
            {
                modelType.HasKey(mt => mt.Id);

                modelType.HasIndex(mt => mt.Name)
                    .IsUnique();

                modelType.Property(mt => mt.Name)
                    .HasMaxLength(EntitiesConstants.ModelTypeNameMaxLength);
            });

            builder.Entity<Option>(option =>
            {
                option.HasKey(o => o.Id);

                option.Property(o => o.Name)
                    .HasMaxLength(EntitiesConstants.OptionNameMaxLength);
            });

            builder.Entity<Order>(order =>
            {
                order.HasKey(uoc => new { uoc.CarId, uoc.UserId });

                order.Property(o => o.Address)
                    .HasMaxLength(EntitiesConstants.OrderAddressMaxLength);

                order.Property(uoc => uoc.OrderDate)
                    .HasDefaultValue(DateTime.UtcNow);
            });

            builder.Entity<Picture>(pictures =>
            {
                pictures.HasKey(p => p.Id);
            });

            builder.Entity<Series>(series =>
            {
                series.HasKey(s => s.Id);

                series.HasIndex(s => s.Name)
                    .IsUnique();

                series.Property(s => s.Name)
                    .HasMaxLength(EntitiesConstants.SeriesNameMaxLength);
            });

            builder.Entity<Transmission>(transmissions =>
            {
                transmissions.HasKey(t => t.Id);

                transmissions.HasMany(t => t.Engines)
                    .WithOne(e => e.Transmission)
                    .HasForeignKey(t => t.TransmissionId);

                transmissions.Property(t => t.Name)
                    .HasMaxLength(EntitiesConstants.TransmissionNameMaxLength);
            });

            builder.Entity<User>(user =>
            {
                user.HasMany(c => c.Orders)
                    .WithOne(bc => bc.User)
                    .HasForeignKey(bc => bc.UserId);

                user.Property(u => u.FirstName)
                    .HasMaxLength(EntitiesConstants.UserNameMaxLength);

                user.Property(u => u.LastName)
                    .HasMaxLength(EntitiesConstants.UserNameMaxLength);

                user.HasMany(u => u.Roles)
                    .WithOne()
                    .HasForeignKey(r => r.UserId);
            });
        }
    }
}