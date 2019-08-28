using BMWStore.Services.Tests.Common;
using BMWStore.Tests.Common.SeedTestMethods;
using System;
using Xunit;

namespace BMWStore.Helpers.Tests.PaginationHelperTests
{
    public class CountTotalPagesCountAsyncTests : BaseTest
    {
        [Fact]
        public async void WithNullEntities_ShouldThrowExcpetion()
        {
            await Assert
                .ThrowsAsync<ArgumentNullException>(async () => await PaginationHelper.CountTotalPagesCountAsync<string>(null));
        }

        [Fact]
        public async void WithoutEntities_ShouldReturnZero()
        {
            var dbContext = this.GetDbContext();

            var countResult = await PaginationHelper.CountTotalPagesCountAsync(dbContext.Series);

            Assert.Equal(0, countResult);
        }

        [Fact]
        public async void WithEntitiesCount_ShouldReturnCorrectEntitiesCount()
        {
            var dbContext = this.GetDbContext();
            SeedSeriesMethods.SeedSeries(dbContext);

            var countResult = await PaginationHelper.CountTotalPagesCountAsync(dbContext.Series);

            Assert.Equal(1, countResult);
        }
    }
}
