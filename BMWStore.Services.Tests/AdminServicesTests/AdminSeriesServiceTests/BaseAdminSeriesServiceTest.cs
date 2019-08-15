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
            var readService = new ReadService(dbContext);
            var adminDeleteService = new AdminDeleteService(dbContext);
            var service = new AdminSeriesService(seriesRepository, readService, adminDeleteService);

            return service;
        }
    }
}
