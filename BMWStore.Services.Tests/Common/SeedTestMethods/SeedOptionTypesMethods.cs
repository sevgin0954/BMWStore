﻿using BMWStore.Data;
using BMWStore.Entities;
using System;

namespace BMWStore.Services.Tests.Common.SeedTestMethods
{
    public static class SeedOptionTypesMethods
    {
        // TODO: Add validation to dbContext.SaveChanges
        public static OptionType SeedOptionType(ApplicationDbContext dbContext)
        {
            var dbOptionType = new OptionType()
            {
                Name = Guid.NewGuid().ToString()
            };

            dbContext.OptionTypes.Add(dbOptionType);
            dbContext.SaveChanges();

            return dbOptionType;
        }
    }
}
