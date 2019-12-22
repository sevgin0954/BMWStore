using BMWStore.Common.Enums;
using BMWStore.Data;
using BMWStore.Entities;
using BMWStore.Services.Models;
using BMWStore.Tests.Common.SeedTestMethods;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace BMWStore.Services.Tests.CarTestDriveServiceTests
{
    public class GetCarTestDriveModelAsyncTests : BaseCarTestDriveServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
		public async Task WithoutSignInUserAndCar_ShouldReturnModelWithFalseIsTestDriveScheduled()
        {
            var dbContext = this.GetDbContext();
			var dbUser = SeedUsersMethods.SeedUser(dbContext);

			var models = await this.CallGetCarsInventoryViewModelAsync(dbContext, true, dbUser.Id);

            Assert.True(models.All(m => m.IsTestDriveScheduled == false));
        }

        [Fact]
		public async Task WithoutSignInUserAndCar_ShouldReturnModelWithNullTestDriveId()
        {
            var dbContext = this.GetDbContext();
			var dbUser = SeedUsersMethods.SeedUser(dbContext);

			var models = await this.CallGetCarsInventoryViewModelAsync(dbContext, true, dbUser.Id);

            Assert.True(models.All(m => m.TestDriveId == null));
        }

        [Fact]
		public async Task WithSignInUserAndWithoutScheduleTestDrive_ShouldReturnModelWithFalseIsTestDriveScheduled()
        {
            var dbContext = this.GetDbContext();
			var dbUser = SeedUsersMethods.SeedUser(dbContext);

			var models = await this.CallGetCarsInventoryViewModelAsync(dbContext, true, dbUser.Id);

            Assert.True(models.All(m => m.IsTestDriveScheduled == false));
        }

        [Fact]
		public async Task WithSignInUserAndWithoutScheduleTestDrive_ShouldReturnModelWithNullTestDriveId()
        {
            var dbContext = this.GetDbContext();
			var dbUser = SeedUsersMethods.SeedUser(dbContext);

			var models = await this.CallGetCarsInventoryViewModelAsync(dbContext, true, dbUser.Id);

            Assert.True(models.All(m => m.TestDriveId == null));
        }

        [Fact]
		public async Task WithSignInUserAndScheduleTestDrive_ShouldReturnModelWithTrueIsTestDriveScheduled()
        {
            var dbContext = this.GetDbContext();
			var dbUser = SeedUsersMethods.SeedUser(dbContext);
			var dbTestDrive = this.ScheduleTestDrive(dbContext, dbUser.Id);

            var models = await this.CallGetCarsInventoryViewModelAsync(dbContext, true, dbUser.Id);

            Assert.Contains(models, m => m.IsTestDriveScheduled);
        }

        [Fact]
        public async Task WithSignInUserAndScheduleTestDrive_ShouldReturnModelWithCorrectTestDriveId()
        {
            var dbContext = this.GetDbContext();
			var dbUser = SeedUsersMethods.SeedUser(dbContext);
            var dbTestDrive = this.ScheduleTestDrive(dbContext, dbUser.Id);

            var models = await this.CallGetCarsInventoryViewModelAsync(dbContext, true, dbUser.Id);

            Assert.True(models.All(m => m.TestDriveId == dbTestDrive.Id));
        }

		private async Task<IEnumerable<CarServiceModel>> CallGetCarsInventoryViewModelAsync(
			ApplicationDbContext dbContext,
			bool isUserSignIn,
			string userId,
			int pageNumber = 1)
		{
			var signInManager = this.GetSetupedSignInManager(isUserSignIn);
			var userManager = this.GetSetupedUserManager(isUserSignIn ? userId : null);
			var service = this.GetService(dbContext, signInManager, userManager);
			var user = new Mock<ClaimsPrincipal>().Object;

			var models = await (await service
				.GetCarTestDriveModelAsync<CarServiceModel>(dbContext.BaseCars, user, pageNumber))
				.ToArrayAsync();

			return models;
		}

		// TODO: Reuse (in GetCarTestDriveModelByIdTests for an example)
		private TestDrive ScheduleTestDrive(ApplicationDbContext dbContext, string userId)
        {
            var upcomingStatus = SeedStatusesMethods.SeedStatus(dbContext, TestDriveStatus.Upcoming);
            var dbTestDrive = SeedTestDrivesMethods.SeedTestDriveWithCar<NewCar>(
                dbContext, 
                userId, 
                upcomingStatus);

            return dbTestDrive;
        }
    }
}
