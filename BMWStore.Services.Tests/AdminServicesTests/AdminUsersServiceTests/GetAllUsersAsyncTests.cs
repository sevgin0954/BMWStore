using BMWStore.Data.SortStrategies.UserStrategies.Interfaces;
using BMWStore.Entities;
using Moq;
using System.Linq;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminUsersServiceTests
{
    public class GetAllUsersAsyncTests : BaseAdminUsersServiceTest, IClassFixture<BaseTestFixture>
    {
        private readonly BaseTestFixture baseTest;

        public GetAllUsersAsyncTests(BaseTestFixture baseTest)
        {
            this.baseTest = baseTest;
        }

        [Fact]
        public async void WithAdminOnly_ShouldReturnEmptyCollection()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);
            this.SeedAdminWithRole(dbContext);
            CommonSeedTestMethods.SeedUserRole(dbContext);

            var sortStrategy = this.GetSortStrategy(dbContext.Users);
            var models = await service.GetAllUsersAsync(sortStrategy);

            Assert.Empty(models);
        }

        [Fact]
        public async void WithAdminAndUser_ShouldReturnUserOnly()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);
            this.SeedUserAndAdmin(dbContext);

            var sortStrategy = this.GetSortStrategy(dbContext.Users);
            var models = await service.GetAllUsersAsync(sortStrategy);

            Assert.Single(models);
        }

        private IUserSortStrategy GetSortStrategy(IQueryable<User> users)
        {
            var mockedSortStrategy = new Mock<IUserSortStrategy>();
            mockedSortStrategy.Setup(ss => ss.Sort(It.IsAny<IQueryable<User>>()))
                .Returns<IQueryable<User>>(i => i);

            return mockedSortStrategy.Object;
        }
    }
}
