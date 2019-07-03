using BMWStore.Models.CarModels.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.Interfaces
{
    public interface INewCarsInvertoryService
    {
        Task<IEnumerable<NewCarConciseViewModel>> GetAllAsync();
    }
}
