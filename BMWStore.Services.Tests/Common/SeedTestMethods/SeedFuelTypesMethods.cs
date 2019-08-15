using BMWStore.Data;
using BMWStore.Entities;
using System;

namespace BMWStore.Services.Tests.Common.SeedTestMethods
{
    public static class SeedFuelTypesMethods
    {
        public static FuelType SeedFuelType(ApplicationDbContext dbContext)
        {
            var dbFuelType = new FuelType()
            {
                Name = Guid.NewGuid().ToString()
            };
            dbContext.FuelTypes.Add(dbFuelType);
            dbContext.SaveChanges();

            return dbFuelType;
        }
    }
}
