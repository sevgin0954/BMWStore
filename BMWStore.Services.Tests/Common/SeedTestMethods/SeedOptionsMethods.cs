using BMWStore.Data;
using BMWStore.Entities;
using System;

namespace BMWStore.Services.Tests.Common.SeedTestMethods
{
    public static class SeedOptionsMethods
    {
        public static Option SeedOption(ApplicationDbContext dbContext)
        {
            var dbOption = new Option()
            {
                Name = Guid.NewGuid().ToString()
            };
            SeedOption(dbContext, dbOption);

            return dbOption;
        }

        public static Option SeedOptionWithOptionType(ApplicationDbContext dbContext)
        {
            var dbOptionType = SeedOptionTypesMethods.SeedOptionType(dbContext);
            var dbOption = new Option()
            {
                Name = Guid.NewGuid().ToString(),
                OptionType = dbOptionType
            };
            SeedOption(dbContext, dbOption);

            return dbOption;
        }

        private static void SeedOption(ApplicationDbContext dbContext, Option option)
        {
            dbContext.Options.Add(option);
            dbContext.SaveChanges();
        }
    }
}
