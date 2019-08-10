using BMWStore.Data;
using BMWStore.Entities;

namespace BMWStore.Services.Tests.Common.SeedTestMethods
{
    public static class SeedEnginesMethods
    {
        public static Engine SeedEngine(ApplicationDbContext dbContext)
        {
            var dbEngine = new Engine();
            dbContext.Engines.Add(dbEngine);
            dbContext.SaveChanges();

            return dbEngine;
        }

        public static Engine SeedEngine(ApplicationDbContext dbContext, string name)
        {
            var dbEngine = new Engine()
            {
                Name = name
            };
            dbContext.Engines.Add(dbEngine);
            dbContext.SaveChanges();

            return dbEngine;
        }

        public static void SeedEngineWithTransmission(ApplicationDbContext dbContext)
        {
            var engine = new Engine();
            var transmission = new Transmission();
            engine.Transmission = transmission;
            dbContext.Engines.Add(engine);
            dbContext.SaveChanges();
        }
    }
}
