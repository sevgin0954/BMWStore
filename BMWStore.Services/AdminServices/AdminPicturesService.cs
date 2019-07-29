using AutoMapper;
using BMWStore.Data.Repositories.Interfaces;
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
        private readonly IPictureRepository pictureRepository;
        private readonly ICloudinaryService cloudinaryService;

        public AdminPicturesService(IPictureRepository pictureRepository, ICloudinaryService cloudinaryService)
        {
            this.pictureRepository = pictureRepository;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task UpdateCarPicturesAsync(BaseCar car, IEnumerable<IFormFile> pictures)
        {
            await this.pictureRepository.RemoveRangeWhereAsync(p => p.CarId == car.Id);

            var pictureUrls = await this.cloudinaryService.UploadPicturesAsync(pictures);
            car.Pictures = Mapper.Map<ICollection<Picture>>(pictureUrls);
        }
    }
}
