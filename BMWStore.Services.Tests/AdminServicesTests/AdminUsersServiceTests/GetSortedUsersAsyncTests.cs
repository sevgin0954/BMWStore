using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminUsersServiceTests
{
    public class GetSortedUsersAsyncTests : BaseAdminUsersServiceTest, IClassFixture<BaseTestFixture>
    {
        private readonly BaseTestFixture baseTest;

        public GetSortedUsersAsyncTests(BaseTestFixture baseTest)
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

            var cookies = new Mock<IRequestCookieCollection>().Object;
            var model = await service.GetSortedUsersAsync(cookies, 1);

            Assert.Empty(model.Users);
        }

        [Fact]
        public async void WithAdminAndUser_ShouldReturnUserOnly()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);
            this.SeedUserAndAdmin(dbContext);

            var cookies = new Mock<IRequestCookieCollection>().Object;
            var model = await service.GetSortedUsersAsync(cookies, 1);

            Assert.Single(model.Users);
        }
    }
}
