using BMWStore.Data;
using BMWStore.Data.Repositories;
using BMWStore.Services.AdminServices;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Tests.Common;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminFuelTypesServiceTests
{
    public abstract class BaseAdminFuelTypesServiceTest : BaseTest
    {
        protected IAdminFuelTypesService GetService(ApplicationDbContext dbContext)
        {
            var fuelTypeRepository = new FuelTypeRepository(dbContext);
            var adminDeleteService = new AdminCommonDeleteService(dbContext);
            var adminEditService = new AdminCommonEditService(dbContext);
            var adminCreateService = new AdminCommonCreateService(dbContext);
            var service = new AdminFuelTypesService(
                fuelTypeRepository, 
                adminDeleteService,
                adminEditService,
                adminCreateService);

            return service;
        }
    }
}
