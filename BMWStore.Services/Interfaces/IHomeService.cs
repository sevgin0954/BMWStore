using BMWStore.Entities;
using BMWStore.Models.HomeModels.BindingModel;
using BMWStore.Models.HomeModels.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services.Interfaces
{
    public interface IHomeService
    {
        Task<HomeSearchBindingModel> GetSearchModelAsync(IQueryable<BaseCar> cars);
    }
}