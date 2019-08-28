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
            var adminDeleteService = new AdminDeleteService(dbContext);
            var adminEditService = new AdminEditService(dbContext);
            var adminCreateService = new AdminCreateService(dbContext);
            var service = new AdminOptionTypesService(
                optionTypeRepository,
                adminDeleteService,
                adminEditService,
                adminCreateService);

            return service;
        }
    }
}
