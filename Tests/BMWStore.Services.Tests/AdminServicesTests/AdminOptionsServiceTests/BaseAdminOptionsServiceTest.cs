using BMWStore.Data;
using BMWStore.Data.Repositories;
using BMWStore.Services.AdminServices;
using BMWStore.Services.AdminServices.Interfaces;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminOptionsServiceTests
{
    public abstract class BaseAdminOptionsServiceTest : BaseTest
    {
        protected IAdminOptionsService GetService(ApplicationDbContext dbContext)
        {
            var optionRepository = new OptionRepository(dbContext);
            var adminDeleteService = new AdminCommonDeleteService(dbContext);
            var adminEditService = new AdminCommonEditService(dbContext);
            var adminCreateService = new AdminCommonCreateService(dbContext);
            var service = new AdminOptionsService(
                optionRepository,
                adminDeleteService,
                adminEditService,
                adminCreateService);

            return service;
        }
    }
}