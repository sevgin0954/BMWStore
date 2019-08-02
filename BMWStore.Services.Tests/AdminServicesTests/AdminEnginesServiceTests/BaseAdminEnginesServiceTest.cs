using BMWStore.Data;
using BMWStore.Data.Repositories;
using BMWStore.Entities;
using BMWStore.Services.AdminServices;
using BMWStore.Services.AdminServices.Interfaces;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminEnginesServiceTests
{
    public abstract class BaseAdminEnginesServiceTest
    {
        protected IAdminEnginesService GetService(ApplicationDbContext dbContext)
        {
            var engineRepository = new EngineRepository(dbContext);
            var selectListItemsService = new SelectListItemsService(dbContext);
            var service = new AdminEnginesService(engineRepository, selectListItemsService);

            return service;
        }

        protected Engine SeedEngine(string id, ApplicationDbContext dbContext)
        {
            var dbEngine = new Engine()
            {
                Id = id
            };
            dbContext.Engines.Add(dbEngine);
            dbContext.SaveChanges();

            return dbEngine;
        }

        protected void SeedEngineWithTransmission(ApplicationDbContext dbContext)
        {
            var engine = new Engine();
            var transmission = new Transmission();
            engine.Transmission = transmission;
            dbContext.Engines.Add(engine);
            dbContext.SaveChanges();
        }
    }
}
