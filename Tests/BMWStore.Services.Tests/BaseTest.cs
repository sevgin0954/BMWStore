using BMWStore.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace BMWStore.Services.Tests
{
    public abstract class BaseTest
    {
        protected ApplicationDbContext GetDbContext()
        {
            var options = this.GetDbContextOptions();
            var dbContext = new ApplicationDbContext(options);

            return dbContext;
        }

        protected DbContextOptions<ApplicationDbContext> GetDbContextOptions()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return options;
        }
    }
}
