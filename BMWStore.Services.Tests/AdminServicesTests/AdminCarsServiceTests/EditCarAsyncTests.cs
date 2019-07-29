using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminCarsServiceTests
{
    public class EditCarAsyncTests : BaseAdminCarsServiceTest, IClassFixture<BaseTestFixture>
    {
        private readonly BaseTestFixture baseTest;

        public EditCarAsyncTests(BaseTestFixture baseTest)
        {
            this.baseTest = baseTest;
        }

        [Fact]
        public void WithIncorrectId_ShouldThrowException()
        {

        }

        [Fact]
        public void WithoutPictures_ShouldNotChangePictures()
        {

        }

        [Fact]
        public void WithPictures_ShouldReplacePictures()
        {

        }

        [Fact]
        public void WithoutOptions_ShouldNotChangeOptions()
        {

        }

        [Fact]
        public void WithOptions_ShouldChangeOptions()
        {

        }
    }
}
