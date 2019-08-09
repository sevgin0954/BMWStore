using BMWStore.Data;
using BMWStore.Entities;

namespace BMWStore.Services.Tests.Common.SeedTestMethods
{
    public static class SeedOptionsMethods
    {
        public static Option SeedOption(ApplicationDbContext dbContext)
        {
            var dbOption = new Option();
            dbContext.Options.Add(dbOption);
            dbContext.SaveChanges();

            return dbOption;
        }
    }
}
