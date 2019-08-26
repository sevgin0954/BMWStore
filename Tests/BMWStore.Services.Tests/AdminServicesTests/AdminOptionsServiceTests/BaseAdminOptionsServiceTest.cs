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
            var readService = new ReadService(dbContext);
            var adminDeleteService = new AdminDeleteService(dbContext);
            var adminEditService = new AdminEditService(dbContext);
            var service = new AdminOptionsService(
                optionRepository, 
                readService, 
                adminDeleteService,
                adminEditService);

            return service;
        }
    }
}