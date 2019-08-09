using BMWStore.Data;
using BMWStore.Data.Repositories;
using BMWStore.Entities;
using BMWStore.Services.AdminServices;
using BMWStore.Services.AdminServices.Interfaces;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminFuelTypesServiceTests
{
    public abstract class BaseAdminFuelTypesServiceTest : BaseTest
    {
        protected IAdminFuelTypesService GetService(ApplicationDbContext dbContext)
        {
            var fuelTypeRepository = new FuelTypeRepository(dbContext);
            var service = new AdminFuelTypesService(fuelTypeRepository);

            return service;
        }
    }
}
