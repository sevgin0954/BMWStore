﻿using BMWStore.Entities;
using BMWStore.Models.CarModels.ViewModels;
using BMWStore.Models.FilterModels.BindingModels;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BMWStore.Services.Tests
{
    public static class CommonMockServicesTestMethods
    {
        public static void SetupCarsService(Mock<ICarsService> mockedCarsService)
        {
            mockedCarsService.Setup(cs => cs.GetCarsInvertoryViewModelAsync(
                    It.IsAny<IQueryable<BaseCar>>(),
                    It.IsAny<ClaimsPrincipal>(),
                    It.IsAny<int>()))
                .ReturnsAsync((IQueryable<BaseCar> cars, ClaimsPrincipal user, int pageNumber) =>
                    cars.Select(c => new CarInvertoryConciseViewModel()).ToList());
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
            mockedCarPriceService.Setup(cps => cps.GetPriceFilterModelsAsync(It.IsAny<IEnumerable<CarInvertoryConciseViewModel>>()))
                .Returns((IEnumerable<CarInvertoryConciseViewModel> input) => Task.FromResult<ICollection<FilterTypeBindingModel>>(
                    input.Select(a => new FilterTypeBindingModel()).ToList())
                );
        }
    }
}
