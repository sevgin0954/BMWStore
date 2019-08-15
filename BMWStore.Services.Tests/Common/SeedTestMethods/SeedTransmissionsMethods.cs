using BMWStore.Data;
using BMWStore.Entities;
using System;

namespace BMWStore.Services.Tests.Common.SeedTestMethods
{
    public static class SeedTransmissionsMethods
    {
        public static Transmission SeedTransmission(ApplicationDbContext dbContext)
        {
            var dbTransmission = new Transmission()
            {
                Name = Guid.NewGuid().ToString(),
                Price = 1
            };
            dbContext.Transmissions.Add(dbTransmission);
            dbContext.SaveChanges();

            return dbTransmission;
        }
    }
}
