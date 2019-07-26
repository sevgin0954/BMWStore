using BMWStore.Common.Constants;
using BMWStore.Entities;
using BMWStore.Models.FilterModels.BindingModels;
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
        public DbSet<CarOption> CarsOptions { get; set; }
        public DbSet<Engine> Engines { get; set; }
        public DbSet<FuelType> FuelTypes { get; set; }
        public DbSet<ModelType> ModelTypes { get; set; }
        public DbSet<NewCar> NewCars { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<TestDrive> TestDrives { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<Transmission> Transmissions { get; set; }
        public DbSet<UsedCar> UsedCars { get; set; }
        public new DbSet<User> Users { get; set; }
        public DbQuery<FilterTypeBindingModel> FilterTypeModels { get; set; }

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

                car.HasMany(c => c.TestDrives)
                    .WithOne(b => b.Car)
                    .HasForeignKey(uc => uc.CarId);

                car.HasMany(c => c.Options)
                    .WithOne(o => o.Car)
                    .HasForeignKey(o => o.CarId);

                car.HasMany(c => c.Pictures)
                    .WithOne(p => p.Car)
                    .HasForeignKey(p => p.CarId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<CarOption>(carOption =>
            {
                carOption.HasKey(co => new { co.CarId, co.OptionId });

                carOption.HasOne(co => co.Car)
                    .WithMany(c => c.Options)
                    .HasForeignKey(co => co.CarId);

                carOption.HasOne(co => co.Option)
                    .WithMany(o => o.Cars)
                    .HasForeignKey(co => co.OptionId);
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

                option.HasMany(o => o.Cars)
                    .WithOne(c => c.Option)
                    .HasForeignKey(c => c.OptionId);
            });

            builder.Entity<TestDrive>(testDrive =>
            {
                testDrive.HasKey(uoc => new { uoc.UserId, uoc.CarId });

                testDrive.Property(uoc => uoc.ScheduleDate)
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

                // TODO: Use attribute
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
                user.HasMany(c => c.TestDrives)
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