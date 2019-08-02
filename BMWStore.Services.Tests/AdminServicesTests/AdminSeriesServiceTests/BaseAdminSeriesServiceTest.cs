using BMWStore.Data;
using BMWStore.Data.Repositories;
using BMWStore.Entities;
using BMWStore.Services.AdminServices;
using BMWStore.Services.AdminServices.Interfaces;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminSeriesServiceTests
{
    public abstract class BaseAdminSeriesServiceTest
    {
        protected IAdminSeriesService GetService(ApplicationDbContext dbContext)
        {
            var seriesRepository = new SeriesRepository(dbContext);
            var service = new AdminSeriesService(seriesRepository);

            return service;
        }

        protected Series SeedSeries(ApplicationDbContext dbContext)
        {
            var dbSeries = new Series();
            dbContext.Series.Add(dbSeries);

            dbContext.SaveChanges();

            return dbSeries;
        }
    }
}
