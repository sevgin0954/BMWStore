using BMWStore.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminPicturesService
    {
        Task UpdateCarPicturesAsync(BaseCar car, IEnumerable<IFormFile> pictures);
    }
}
