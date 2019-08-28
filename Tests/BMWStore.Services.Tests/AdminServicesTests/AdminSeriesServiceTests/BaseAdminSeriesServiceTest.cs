using BMWStore.Data;
using BMWStore.Data.Repositories;
using BMWStore.Services.AdminServices;
using BMWStore.Services.AdminServices.Interfaces;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminSeriesServiceTests
{
    public abstract class BaseAdminSeriesServiceTest : BaseTest
    {
        protected IAdminSeriesService GetService(ApplicationDbContext dbContext)
        {
            var seriesRepository = new SeriesRepository(dbContext);
            var adminDeleteService = new AdminCommonDeleteService(dbContext);
            var adminEditService = new AdminCommonEditService(dbContext);
            var adminCreateService = new AdminCommonCreateService(dbContext);
            var service = new AdminSeriesService(
                seriesRepository,
                adminDeleteService,
                adminEditService,
                adminCreateService);

            return service;
        }
    }
}
