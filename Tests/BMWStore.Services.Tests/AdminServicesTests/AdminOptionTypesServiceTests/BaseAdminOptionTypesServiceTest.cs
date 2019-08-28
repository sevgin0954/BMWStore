using BMWStore.Data;
using BMWStore.Data.Repositories;
using BMWStore.Services.AdminServices;
using BMWStore.Services.AdminServices.Interfaces;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminOptionTypesServiceTests
{
    public abstract class BaseAdminOptionTypesServiceTest : BaseTest
    {
        public IAdminOptionTypesService GetService(ApplicationDbContext dbContext)
        {
            var optionTypeRepository = new OptionTypeRepository(dbContext);
            var adminDeleteService = new AdminCommonDeleteService(dbContext);
            var adminEditService = new AdminCommonEditService(dbContext);
            var adminCreateService = new AdminCommonCreateService(dbContext);
            var service = new AdminOptionTypesService(
                optionTypeRepository,
                adminDeleteService,
                adminEditService,
                adminCreateService);

            return service;
        }
    }
}
