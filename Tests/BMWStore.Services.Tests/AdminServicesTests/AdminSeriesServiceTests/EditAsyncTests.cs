﻿using BMWStore.Common.Constants;
using BMWStore.Tests.Common.CreateMethods;
using BMWStore.Tests.Common.SeedTestMethods;
using System;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminSeriesServiceTests
{
    public class EditAsyncTests : BaseAdminSeriesServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithIncorrectId_ShouldThrowException()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var incorrectId = Guid.NewGuid().ToString();
            var model = SeriesServiceModelMethods.Create(incorrectId);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await service.EditAsync(model));
            Assert.Equal(ErrorConstants.IncorrectId, exception.Message);
        }

        [Fact]
        public async void WithCorrectId_ShouldEditSeriesName()
        {
            var dbContext = this.GetDbContext();
            var dbSeries = SeedSeriesMethods.SeedSeries(dbContext);
            var service = this.GetService(dbContext);
            var model = SeriesServiceModelMethods.Create(dbSeries.Id);

            await service.EditAsync(model);

            Assert.Equal(model.Name, dbSeries.Name);
        }
    }
}