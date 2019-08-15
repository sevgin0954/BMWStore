using BMWStore.Data;
using BMWStore.Entities;
using System;

namespace BMWStore.Services.Tests.Common.SeedTestMethods
{
    public static class SeedSeriesMethods
    {
        public static Series SeedSeries(ApplicationDbContext dbContext)
        {
            var dbSeries = new Series()
            {
                Name = Guid.NewGuid().ToString()
            };
            dbContext.Series.Add(dbSeries);
            dbContext.SaveChanges();

            return dbSeries;
        }
    }
}
