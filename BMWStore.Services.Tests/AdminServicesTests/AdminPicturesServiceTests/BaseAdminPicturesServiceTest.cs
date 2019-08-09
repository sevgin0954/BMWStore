using BMWStore.Data;
using BMWStore.Data.Repositories;
using BMWStore.Entities;
using BMWStore.Services.AdminServices;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Interfaces;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminPicturesServiceTests
{
    public abstract class BaseAdminPicturesServiceTest : BaseTest
    {
        public IAdminPicturesService GetService(ApplicationDbContext dbContext, ICloudinaryService cloudinaryService)
        {
            var pictureRepository = new PictureRepository(dbContext);
            return new AdminPicturesService(pictureRepository, cloudinaryService);
        }

        public Picture CreatePicture(ApplicationDbContext dbContext)
        {
            var dbPicture = new Picture();
            dbContext.Pictures.Add(dbPicture);

            return dbPicture;
        }
    }
}
