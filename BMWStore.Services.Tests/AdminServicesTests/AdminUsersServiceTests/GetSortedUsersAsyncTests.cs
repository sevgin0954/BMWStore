using BMWStore.Tests.Common.SeedTestMethods;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminUsersServiceTests
{
    public class GetSortedUsersAsyncTests : BaseAdminUsersServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithAdminOnly_ShouldReturnEmptyCollection()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            SeedUsersMethods.SeedAdminWithRole(dbContext);
            SeedRolesMethods.SeedUserRole(dbContext);

            var cookies = new Mock<IRequestCookieCollection>().Object;
            var model = await service.GetSortedUsersAsync(cookies, 1);

            Assert.Empty(model.Users);
        }

        [Fact]
        public async void WithAdminAndUser_ShouldReturnUserOnly()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            this.SeedUserAndAdmin(dbContext);

            var cookies = new Mock<IRequestCookieCollection>().Object;
            var model = await service.GetSortedUsersAsync(cookies, 1);

            Assert.Single(model.Users);
        }
    }
}
