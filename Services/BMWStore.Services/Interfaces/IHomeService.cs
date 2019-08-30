using BMWStore.Common.Enums;
using BMWStore.Entities;
using BMWStore.Services.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services.Interfaces
{
    public interface IHomeService
    {
        Task<HomeSearchServiceModel> GetSearchModelAsync(IQueryable<BaseCar> cars, CarType carType);
    }
}