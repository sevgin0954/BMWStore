using BMWStore.Data;
using BMWStore.Entities;

namespace BMWStore.Services.Tests.Common.SeedTestMethods
{
    public static class SeedSeriesMethods
    {
        public static Series SeedSeries(ApplicationDbContext dbContext)
        {
            var dbSeries = new Series();
            dbContext.Series.Add(dbSeries);
            dbContext.SaveChanges();

            return dbSeries;
        }
    }
}
