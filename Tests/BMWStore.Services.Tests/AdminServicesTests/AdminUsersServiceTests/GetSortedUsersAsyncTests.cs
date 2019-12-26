using BMWStore.Data;
using BMWStore.Services.SortStrategies.UserStrategies.Interfaces;
using BMWStore.Services.Models;
using BMWStore.Tests.Common.MockMethods;
using BMWStore.Tests.Common.SeedTestMethods;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminUsersServiceTests
{
    public class GetSortedUsersAsyncTests : BaseAdminUsersServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithEmptyUsersCollection_ShouldReturnEmptyCollection()
        {
            var dbContext = this.GetDbContext();
            SeedRolesMethods.SeedUserRole(dbContext);

            var model = await this.CallGetSortedUsersAsync(dbContext);

            Assert.Empty(model);
        }

        [Fact]
        public async void WithBiggerPageNumber_ShouldEmptyCollection()
        {
            var dbContext = this.GetDbContext();
            SeedUsersMethods.SeedUserWithRole(dbContext);
            var pageNumber = 2;

            var model = await this.CallGetSortedUsersAsync(dbContext, pageNumber);

            Assert.Empty(model);
        }

        [Fact]
        public async void WithAdminOnly_ShouldReturnEmptyCollection()
        {
            var dbContext = this.GetDbContext();
            SeedUsersMethods.SeedAdminWithRole(dbContext);
            SeedRolesMethods.SeedUserRole(dbContext);

            var model = await this.CallGetSortedUsersAsync(dbContext);

            Assert.Empty(model);
        }

        [Fact]
        public async void WithAdminAndUser_ShouldReturnUserOnly()
        {
            var dbContext = this.GetDbContext();
            this.SeedUserAndAdmin(dbContext);

            var model = await this.CallGetSortedUsersAsync(dbContext);

            Assert.Single(model);
        }

        private async Task<IQueryable<UserConciseServiceModel>> CallGetSortedUsersAsync(ApplicationDbContext dbContext, int pageNumber = 1)
        {
            var service = this.GetService(dbContext);
            var mockedSortStrategy = new Mock<IUserSortStrategy>();
			CommonSetupMockMethods.SetupMockedUserSortStrategy(mockedSortStrategy);
            var model = await service.GetSortedUsersAsync(mockedSortStrategy.Object, pageNumber);

            return model;
        }
    }
}