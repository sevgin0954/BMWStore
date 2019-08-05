using BMWStore.Entities;
using BMWStore.Models.CarModels.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services.Interfaces
{
    public interface ICarsService
    {
        Task<IEnumerable<CarConciseViewModel>> GetAllCarsAsync(
            IQueryable<BaseCar> cars,
            int pageNumber);
    }
}
