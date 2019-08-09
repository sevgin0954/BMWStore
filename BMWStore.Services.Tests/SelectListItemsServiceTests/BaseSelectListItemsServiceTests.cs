using BMWStore.Data;
using BMWStore.Services.Interfaces;

namespace BMWStore.Services.Tests.SelectListItemsServiceTests
{
    public abstract class BaseSelectListItemsServiceTests : BaseTest
    {
        public ISelectListItemsService GetService(ApplicationDbContext dbContext)
        {
            var service = new SelectListItemsService(dbContext);

            return service;
        }
    }
}
