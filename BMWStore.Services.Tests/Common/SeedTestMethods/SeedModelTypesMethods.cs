using BMWStore.Data;
using BMWStore.Entities;

namespace BMWStore.Services.Tests.Common.SeedTestMethods
{
    public static class SeedModelTypesMethods
    {
        public static ModelType SeedModelType(ApplicationDbContext dbContext)
        {
            var dbModelType = new ModelType();
            dbContext.ModelTypes.Add(dbModelType);
            dbContext.SaveChanges();

            return dbModelType;
        }
    }
}
