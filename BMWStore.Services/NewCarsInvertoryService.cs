using BMWStore.Data.FilterStrategies.CarStrategies.Interfaces;
using BMWStore.Data.Interfaces;
using BMWStore.Data.SortStrategies.CarsStrategies.Interfaces;
using BMWStore.Models.CarInvertoryModels.ViewModels;
using BMWStore.Models.FilterModels.BindingModels;
using BMWStore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services
{
    public class NewCarsInvertoryService : INewCarsInvertoryService
    {
        private readonly IBMWStoreUnitOfWork unitOfWork;
        private readonly ICarsService carsService;

        public NewCarsInvertoryService(IBMWStoreUnitOfWork unitOfWork, ICarsService carsService)
        {
            this.unitOfWork = unitOfWork;
            this.carsService = carsService;
        }

        public async Task<NewCarsInvertoryViewModel> GetInvertoryBindingModel(
            ICarSortStrategy sortStrategy, 
            params ICarFilterStrategy[] filterStrategies)
        {
            var carModels = await this.carsService.GetAllNewCarsAsync(sortStrategy, filterStrategies);

            var yearModels = await this.unitOfWork.NewCars.GetAll()
                .GroupBy(c => c.Year)
                .Select(c => new FilterTypeBindingModel()
                {
                    CarsCount = c.Count(),
                    Value = c.Key,
                    Text = c.Key
                })
                .ToArrayAsync();

            var seriesModels = await this.unitOfWork.NewCars.GetAll()
                .GroupBy(c => new { Value = c.Series.Id, Text = c.Series.Name })
                .Select(c => new FilterTypeBindingModel()
                {
                    CarsCount = c.Count(),
                    Value = c.Key.Value,
                    Text = c.Key.Text
                })
                .ToArrayAsync();

            var modelTypeModels = await this.unitOfWork.NewCars.GetAll()
                .GroupBy(c => new { Value = c.ModelType.Id, Text = c.ModelType.Name })
                .Select(c => new FilterTypeBindingModel()
                {
                    CarsCount = c.Count(),
                    Value = c.Key.Value,
                    Text = c.Key.Text
                })
                .ToArrayAsync();

            var carType = new SqlParameter("type", "NewCar");
            var priceModels = await this.unitOfWork.Query<FilterTypeBindingModel>()
                .FromSql("EXECUTE usp_GetCarPriceRangesCount @carType=@type", carType)
                .ToArrayAsync();

            var model = new NewCarsInvertoryViewModel()
            {
                Cars = carModels,
                Years = yearModels,
                Series = seriesModels,
                ModelTypes = modelTypeModels,
                Prices = priceModels
            };

            return model;
        }
    }
}
