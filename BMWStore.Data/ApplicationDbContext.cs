using BMWStore.Common.Constants;
using BMWStore.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BMWStore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Color> Colors { get; set; }
        public DbSet<Engine> Engines { get; set; }
        public DbSet<Exterior> Exteriors { get; set; }
        public DbSet<FuelType> FuelTypes { get; set; }
        public DbSet<ModelType> ModelTypes { get; set; }
        public DbSet<NewCar> NewCars { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<Transmission> Transmissions { get; set; }
        public DbSet<UsedCar> UsedCars { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BaseCar>(car =>
            {
                car.HasKey(c => c.Id);

                car.HasOne(c => c.Engine)
                    .WithMany(e => e.Cars)
                    .HasForeignKey(c => c.EngineId);

                car.HasOne(c => c.Exterior)
                    .WithMany(e => e.Cars)
                    .HasForeignKey(c => c.ExteriorId);

                car.HasOne(c => c.FuelType)
                    .WithMany(ft => ft.Cars)
                    .HasForeignKey(c => c.FuelTypeId);

                car.HasOne(c => c.ModelType)
                    .WithMany(mt => mt.Cars)
                    .HasForeignKey(c => c.ModelTypeId);

                car.HasOne(c => c.Series)
                    .WithMany(s => s.Cars)
                    .HasForeignKey(c => c.SeriesId);
            });

            builder.Entity<Color>(color =>
            {
                color.HasKey(c => c.Id);

                color.Property(c => c.Name)
                .HasMaxLength(EntitiesConstants.ColorNameMaxLength);;
            });

            builder.Entity<Engine>(engine =>
            {
                engine.HasKey(e => e.Id);

                engine.HasOne(e => e.Transmission)
                    .WithMany(t => t.Engines)
                    .HasForeignKey(e => e.TransmissionId);
            });

            builder.Entity<Exterior>(exterior =>
            {
                exterior.HasKey(e => e.Id);

                exterior.HasOne(e => e.Color)
                    .WithMany(c => c.Exteriors)
                    .HasForeignKey(e => e.ColorId);
            });

            builder.Entity<FuelType>(fuelType =>
            {
                fuelType.HasKey(ft => ft.Id);

                fuelType.Property(ft => ft.Name)
                    .HasMaxLength(EntitiesConstants.FuelTypeNameMaxLength);
            });

            builder.Entity<ModelType>(modelType =>
            {
                modelType.HasKey(mt => mt.Id);

                modelType.Property(mt => mt.Name)
                    .HasMaxLength(EntitiesConstants.ModelTypeNameMaxLength);
            });

            builder.Entity<Option>(option =>
            {
                option.HasKey(o => o.Id);

                option.Property(o => o.Name)
                    .HasMaxLength(EntitiesConstants.OptionNameMaxLength);
            });

            builder.Entity<Picture>(pictures =>
            {
                pictures.HasKey(p => p.Id);

                pictures.HasOne(p => p.Exterior)
                    .WithMany(e => e.Pictures)
                    .HasForeignKey(p => p.ExteriorId);
            });

            builder.Entity<Series>(series =>
            {
                series.HasKey(s => s.Id);

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

            base.OnModelCreating(builder);
        }
    }
}
