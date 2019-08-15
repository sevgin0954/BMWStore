using BMWStore.Data;
using BMWStore.Entities;
using System;

namespace BMWStore.Services.Tests.Common.SeedTestMethods
{
    public static class SeedModelTypesMethods
    {
        public static ModelType SeedModelType(ApplicationDbContext dbContext)
        {
            var dbModelType = new ModelType()
            {
                Name = Guid.NewGuid().ToString()
            };
            dbContext.ModelTypes.Add(dbModelType);
            dbContext.SaveChanges();

            return dbModelType;
        }
    }
}
