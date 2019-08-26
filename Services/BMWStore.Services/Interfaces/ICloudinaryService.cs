using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.Interfaces
{
    public interface ICloudinaryService
    {
        Task<IEnumerable<string>> UploadPicturesAsync(IEnumerable<IFormFile> pictureFile);
        Task<string> UploadPictureAsync(IFormFile pictureFile);
    }
}
