using BMWStore.Common.Constants;
using System;
using Xunit;

namespace BMWStore.Services.Tests.CarsServiceTests
{
	public class GetByIdAsyncTests : BaseCarsServiceTest, IClassFixture<MapperFixture>
	{
		[Fact]
		public async void WithIncorrectCarId_ShouldThrowException()
		{
			var dbContext = this.GetDbContext();
			var service = this.GetService(dbContext);

			var incorrectCarId = Guid.NewGuid().ToString();

			var exception = 
				await Assert.ThrowsAsync<ArgumentException>(async () => await service.GetByIdAsync(incorrectCarId));
			Assert.Equal(ErrorConstants.IncorrectId, exception.Message);
		}
	}
}
