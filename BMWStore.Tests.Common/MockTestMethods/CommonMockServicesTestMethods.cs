using BMWStore.Entities;
using BMWStore.Models.CarModels.ViewModels;
using BMWStore.Models.FilterModels.BindingModels;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BMWStore.Tests.Common.MockTestMethods
{
    public static class CommonMockServicesTestMethods
    {
        public static void SetupCarsService(Mock<ICarsService> mockedCarsService)
        {
            mockedCarsService.Setup(cs => cs.GetCarsInventoryViewModelAsync(
                    It.IsAny<IQueryable<BaseCar>>(),
                    It.IsAny<ClaimsPrincipal>(),
                    It.IsAny<int>()))
                .ReturnsAsync((IQueryable<BaseCar> cars, ClaimsPrincipal user, int pageNumber) =>
                    cars.Select(c => new CarInventoryConciseViewModel()).ToList());
        }

        public static void SetupMockedCludinaryServiceUploadPicturesAsync(
            Mock<ICloudinaryService> mock,
            params string[] returnPictureUrls)
        {
            mock.Setup(cs => cs.UploadPicturesAsync(It.IsAny<IEnumerable<IFormFile>>()))
                .ReturnsAsync(returnPictureUrls);
        }

        public static void SetupCarPriceService(Mock<ICarPriceService> mockedCarPriceService)
        {
            mockedCarPriceService.Setup(cps => cps.GetPriceFilterModelsAsync(It.IsAny<IEnumerable<CarInventoryConciseViewModel>>()))
                .Returns((IEnumerable<CarInventoryConciseViewModel> input) => Task.FromResult<ICollection<FilterTypeBindingModel>>(
                    input.Select(a => new FilterTypeBindingModel()).ToList())
                );
        }
    }
}
