using BMWStore.Models.HomeModels.ViewModels;
using System.Threading.Tasks;

namespace BMWStore.Services.Interfaces
{
    public interface IHomeService
    {
        Task<HomeViewModel> GetHomeModelAsync();
    }
}