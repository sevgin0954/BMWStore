using BMWStore.Models.CarModels.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.Interfaces
{
    public interface IAdminCarsService
    {
        Task<IEnumerable<CarConciseViewModel>> GetAllCarsAsync();
    }
}
