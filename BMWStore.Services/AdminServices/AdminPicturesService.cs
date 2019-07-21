using AutoMapper;
using BMWStore.Data.Interfaces;
using BMWStore.Entities;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices
{
    public class AdminPicturesService : IAdminPicturesService
    {
        private readonly IBMWStoreUnitOfWork unitOfWork;
        private readonly ICloudinaryService cloudinaryService;

        public AdminPicturesService(IBMWStoreUnitOfWork unitOfWork, ICloudinaryService cloudinaryService)
        {
            this.unitOfWork = unitOfWork;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task UpdateCarPicturesAsync(BaseCar car, IEnumerable<IFormFile> pictures)
        {
            await this.unitOfWork.Pictures.RemoveWithCarIdAsync(car.Id);

            var pictureUrls = await this.cloudinaryService.UploadPicturesAsync(pictures);
            car.Pictures = Mapper.Map<ICollection<Picture>>(pictureUrls);
        }
    }
}
