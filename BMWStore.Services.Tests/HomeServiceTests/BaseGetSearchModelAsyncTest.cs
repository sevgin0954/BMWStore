using BMWStore.Data;
using BMWStore.Data.Repositories;
using BMWStore.Services.Interfaces;

namespace BMWStore.Services.Tests.HomeServiceTests
{
    public abstract class BaseGetSearchModelAsyncTest
    {

        public IHomeService GetService(ApplicationDbContext dbContext)
        {
            var carRepository = new CarRepository(dbContext);
            var carInventoriesService = new CarInventoriesService();
            var carYearService = new CarYearService();
            var carModelTypeService = new CarModelTypeService();
            var carPriceService = new CarPriceService(dbContext);
            var service = new HomeService(
                carRepository, 
                carInventoriesService, 
                carYearService, 
                carModelTypeService, 
                carPriceService);

            return service;
        }
    }
}
