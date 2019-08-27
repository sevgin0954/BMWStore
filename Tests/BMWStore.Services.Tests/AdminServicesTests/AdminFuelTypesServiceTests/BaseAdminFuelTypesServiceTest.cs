using BMWStore.Data;
using BMWStore.Data.Repositories;
using BMWStore.Services.AdminServices;
using BMWStore.Services.AdminServices.Interfaces;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminFuelTypesServiceTests
{
    public abstract class BaseAdminFuelTypesServiceTest : BaseTest
    {
        protected IAdminFuelTypesService GetService(ApplicationDbContext dbContext)
        {
            var fuelTypeRepository = new FuelTypeRepository(dbContext);
            var adminDeleteService = new AdminDeleteService(dbContext);
            var adminEditService = new AdminEditService(dbContext);
            var service = new AdminFuelTypesService(
                fuelTypeRepository, 
                adminDeleteService,
                adminEditService);

            return service;
        }
    }
}
