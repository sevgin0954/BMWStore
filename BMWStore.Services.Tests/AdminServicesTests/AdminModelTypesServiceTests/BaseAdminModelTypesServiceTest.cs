using BMWStore.Data;
using BMWStore.Data.Repositories;
using BMWStore.Entities;
using BMWStore.Services.AdminServices;
using BMWStore.Services.AdminServices.Interfaces;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminModelTypesServiceTests
{
    public abstract class BaseAdminModelTypesServiceTest
    {
        public IAdminModelTypesService GetService(ApplicationDbContext dbContext)
        {
            var modelTypeRepository = new ModelTypeRepository(dbContext);
            var service = new AdminModelTypesService(modelTypeRepository);

            return service;
        }

        protected ModelType SeedModelType(ApplicationDbContext dbContext)
        {
            var dbModelType = new ModelType();
            dbContext.ModelTypes.Add(dbModelType);

            dbContext.SaveChanges();

            return dbModelType;
        }
    }
}
