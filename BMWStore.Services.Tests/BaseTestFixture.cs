using BMWStore.Data;
using BMWStore.Entities;
using BMWStore.Models;
using MappingRegistrar;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

namespace BMWStore.Services.Tests
{
    public class BaseTestFixture
    {
        public BaseTestFixture()
        {
            RegisterAutoMapperMappings.RegisterMappings(
                typeof(ErrorViewModel).GetTypeInfo().Assembly,
                typeof(User).GetTypeInfo().Assembly);
        }

        public ApplicationDbContext GetDbContext()
        {
            var options = this.GetDbContextOptions();
            var dbContext = new ApplicationDbContext(options);
            
            return dbContext;
        }

        private DbContextOptions<ApplicationDbContext> GetDbContextOptions()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return options;
        }
    }
}
