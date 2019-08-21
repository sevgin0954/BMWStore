using BMWStore.Data;
using BMWStore.Models.AdminModels.ViewModels;
using BMWStore.Tests.Common.SeedTestMethods;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminUsersServiceTests
{
    public class GetSortedUsersAsyncTests : BaseAdminUsersServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithAdminOnly_ShouldReturnEmptyCollection()
        {
            var dbContext = this.GetDbContext();
            SeedUsersMethods.SeedAdminWithRole(dbContext);
            SeedRolesMethods.SeedUserRole(dbContext);

            var model = await this.CallGetSortedUsersAsync(dbContext);

            Assert.Empty(model.Users);
        }

        [Fact]
        public async void WithAdminAndUser_ShouldReturnUserOnly()
        {
            var dbContext = this.GetDbContext();
            this.SeedUserAndAdmin(dbContext);

            var model = await this.CallGetSortedUsersAsync(dbContext);

            Assert.Single(model.Users);
        }

        [Fact]
        public async void WithBannedUser_ShouldReturnModelWithCorrectIsBannedPropery()
        {
            var dbContext = this.GetDbContext();
            var dbUser = SeedUsersMethods.SeedUserWithRole(dbContext);
            this.BanUser(dbContext, dbUser);

            var model = await this.CallGetSortedUsersAsync(dbContext);

            Assert.True(model.Users.First().IsBanned);
        }

        [Fact]
        public async void WithNotBannedUser_ShouldReturnModelWithCorrectIsBannedPropery()
        {
            var dbContext = this.GetDbContext();
            var dbUser = SeedUsersMethods.SeedUserWithRole(dbContext);

            var model = await this.CallGetSortedUsersAsync(dbContext);

            Assert.False(model.Users.First().IsBanned);
        }

        private async Task<AdminUsersViewModel> CallGetSortedUsersAsync(ApplicationDbContext dbContext, int pageNumber = 1)
        {
            var service = this.GetService(dbContext);
            var cookies = new Mock<IRequestCookieCollection>().Object;
            var model = await service.GetSortedUsersAsync(cookies, pageNumber);

            return model;
        }
    }
}
