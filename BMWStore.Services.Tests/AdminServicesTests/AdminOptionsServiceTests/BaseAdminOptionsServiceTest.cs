using BMWStore.Data;
using BMWStore.Data.Repositories;
using BMWStore.Entities;
using BMWStore.Services.AdminServices;
using BMWStore.Services.AdminServices.Interfaces;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminOptionsServiceTests
{
    public abstract class BaseAdminOptionsServiceTest
    {
        protected IAdminOptionsService GetService(ApplicationDbContext dbContext)
        {
            var optionRepository = new OptionRepository(dbContext);
            return new AdminOptionsService(optionRepository);
        }

        protected Option SeedOption(ApplicationDbContext dbContext)
        {
            var dbOption = new Option();
            dbContext.Options.Add(dbOption);

            dbContext.SaveChanges();

            return dbOption;
        }
    }
}
