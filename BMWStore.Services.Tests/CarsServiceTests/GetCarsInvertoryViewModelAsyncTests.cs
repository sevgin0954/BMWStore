using BMWStore.Common.Enums;
using BMWStore.Data;
using BMWStore.Entities;
using BMWStore.Models.CarModels.ViewModels;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace BMWStore.Services.Tests.CarsServiceTests
{
    public class GetCarsInvertoryViewModelAsyncTests : BaseCarsServiceTest, IClassFixture<BaseTestFixture>
    {
        private readonly BaseTestFixture baseTest;

        public GetCarsInvertoryViewModelAsyncTests(BaseTestFixture baseTest)
        {
            this.baseTest = baseTest;
        }

        [Fact]
        public async void WithoutSignInUserAndCar_ShouldReturnModelWithFalseIsTestDriveScheduled()
        {
            var dbContext = this.baseTest.GetDbContext();

            var models = await this.CallGetCarsInvertoryViewModelAsync(dbContext, true);

            Assert.True(models.All(m => m.IsTestDriveScheduled == false));
        }

        [Fact]
        public async void WithoutSignInUserAndCar_ShouldReturnModelWithNullTestDriveId()
        {
            var dbContext = this.baseTest.GetDbContext();

            var models = await this.CallGetCarsInvertoryViewModelAsync(dbContext, true);

            Assert.True(models.All(m => m.TestDriveId == null));
        }

        [Fact]
        public async void WithSignInUserAndWithoutScheduleTestDrive_ShouldReturnModelWithFalseIsTestDriveScheduled()
        {
            var dbContext = this.baseTest.GetDbContext();

            var models = await this.CallGetCarsInvertoryViewModelAsync(dbContext, true);

            Assert.True(models.All(m => m.IsTestDriveScheduled == false));
        }

        [Fact]
        public async void WithSignInUserAndWithoutScheduleTestDrive_ShouldReturnModelWithNullTestDriveId()
        {
            var dbContext = this.baseTest.GetDbContext();

            var models = await this.CallGetCarsInvertoryViewModelAsync(dbContext, true);

            Assert.True(models.All(m => m.TestDriveId == null));
        }

        [Fact]
        public async void WithSignInUserAndScheduleTestDrive_ShouldReturnModelWithTrueIsTestDriveScheduled()
        {
            var dbContext = this.baseTest.GetDbContext();
            this.ScheduleTestDrive(dbContext);

            var models = await this.CallGetCarsInvertoryViewModelAsync(dbContext, true);

            Assert.True(models.All(m => m.IsTestDriveScheduled));
        }

        [Fact]
        public async void WithSignInUserAndScheduleTestDrive_ShouldReturnModelWithCorrectTestDriveId()
        {
            var dbContext = this.baseTest.GetDbContext();
            var dbTestDrive = this.ScheduleTestDrive(dbContext);

            var models = await this.CallGetCarsInvertoryViewModelAsync(dbContext, true);

            Assert.True(models.All(m => m.TestDriveId == dbTestDrive.Id));
        }

        private async Task<IEnumerable<CarInvertoryConciseViewModel>> CallGetCarsInvertoryViewModelAsync(
            ApplicationDbContext dbContext,
            bool isUserSignIn, 
            int pageNumber = 1)
        {
            var signInManager = this.GetSetupedSignInManager(true);
            var service = this.GetService(signInManager);
            var user = new Mock<ClaimsPrincipal>().Object;

            var models = await service.GetCarsInvertoryViewModelAsync(dbContext.BaseCars, user, 1);

            return models;
        }

        private SignInManager<User> GetSetupedSignInManager(bool isSignIn)
        {
            var mockedSignInManager = CommonMockTestMethods.GetMockedSignInManager();
            CommonMockTestMethods.SetupMockedSignInManager(mockedSignInManager, isSignIn);

            return mockedSignInManager.Object;
        }

        private TestDrive ScheduleTestDrive(ApplicationDbContext dbContext)
        {
            var dbTestDrive = CommonSeedTestMethods.SeedTestDriveWithCar<NewCar>(dbContext, "", TestDriveStatus.Upcoming);

            return dbTestDrive;
        }
    }
}
