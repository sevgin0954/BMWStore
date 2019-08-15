using BMWStore.Data;
using BMWStore.Entities;

namespace BMWStore.Services.Tests.Common.SeedTestMethods
{
    public static class SeedEnginesMethods
    {
        public static Engine SeedEngine(ApplicationDbContext dbContext)
        {
            var dbEngine = new Engine();
            AddEngine(dbContext, dbEngine);

            return dbEngine;
        }

        public static Engine SeedEngine(ApplicationDbContext dbContext, string name)
        {
            var dbEngine = new Engine()
            {
                Name = name
            };
            AddEngine(dbContext, dbEngine);

            return dbEngine;
        }

        public static Engine SeedEngineWithTransmission(ApplicationDbContext dbContext)
        {
            var dbEngine = new Engine();
            var transmission = new Transmission();
            dbEngine.Transmission = transmission;

            AddEngine(dbContext, dbEngine);

            return dbEngine;
        }

        private static void AddEngine(ApplicationDbContext dbContext, Engine engine)
        {
            dbContext.Engines.Add(engine);
            dbContext.SaveChanges();
        }
    }
}
