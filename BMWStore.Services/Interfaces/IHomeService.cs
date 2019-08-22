using BMWStore.Common.Enums;
using BMWStore.Entities;
using BMWStore.Models.HomeModels.BindingModel;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services.Interfaces
{
    public interface IHomeService
    {
        Task<HomeSearchBindingModel> GetSearchModelAsync(IQueryable<BaseCar> cars, CarType carType);
    }
}