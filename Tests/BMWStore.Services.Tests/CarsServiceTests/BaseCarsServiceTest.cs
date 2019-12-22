using BMWStore.Data;
using BMWStore.Data.Repositories;
using BMWStore.Services.Interfaces;
using BMWStore.Services.Tests.Common;

namespace BMWStore.Services.Tests.CarsServiceTests
{
    public abstract class BaseCarsServiceTest : BaseTest
    {
        protected ICarsService GetService(ApplicationDbContext dbContext)
        {
			var carRepository = new CarRepository(dbContext);
			var service = new CarsService(carRepository);

			return service;
        }
    }
}
