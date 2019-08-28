using BMWStore.Entities;
using BMWStore.Services.Interfaces;
using BMWStore.Services.Models;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BMWStore.Tests.Common.MockTestMethods
{
    public static class CommonMockServicesMethods
    {
        //public static void SetupCarsService(Mock<ICarsService> mockedCarsService)
        //{
        //    mockedCarsService.Setup(cs => cs.GetCarTestDriveModelAsync<CarServiceModel>(
        //            It.IsAny<IQueryable<BaseCar>>(),
        //            It.IsAny<ClaimsPrincipal>(),
        //            It.IsAny<int>()))
        //        .ReturnsAsync((IQueryable<BaseCar> cars, ClaimsPrincipal user, int pageNumber) =>
        //            cars.Select(c => new CarServiceModel()).ToList());
        //}

        //public static void SetupMockedCludinaryServiceUploadPicturesAsync(
        //    Mock<ICloudinaryService> mock,
        //    params string[] returnPictureUrls)
        //{
        //    mock.Setup(cs => cs.UploadPicturesAsync(It.IsAny<IEnumerable<IFormFile>>()))
        //        .ReturnsAsync(returnPictureUrls);
        //}

        public static void SetupCarPriceService(Mock<ICarPriceService> mockedCarPriceService)
        {
            mockedCarPriceService.Setup(cps => cps.GetPriceFilterModelsAsync(It.IsAny<IQueryable<BaseCar>>()))
                .Returns((IQueryable<BaseCar> input) => Task.FromResult<ICollection<FilterTypeServiceModel>>(
                    input.Select(a => new FilterTypeServiceModel()).ToList())
                );
        }
    }
}
