using BMWStore.Data;
using BMWStore.Entities;

namespace BMWStore.Services.Tests.Common.SeedTestMethods
{
    public static class SeedFuelTypesMethods
    {
        public static FuelType SeedFuelType(ApplicationDbContext dbContext)
        {
            var dbFuelType = new FuelType();
            dbContext.FuelTypes.Add(dbFuelType);
            dbContext.SaveChanges();

            return dbFuelType;
        }
    }
}
