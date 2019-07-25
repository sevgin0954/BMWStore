using BMWStore.Data.FilterStrategies.CarStrategies.Interfaces;
using BMWStore.Data.Interfaces;
using BMWStore.Data.SortStrategies.CarsStrategies.Interfaces;
using BMWStore.Models.CarInvertoryModels.ViewModels;
using BMWStore.Models.CarModels.ViewModels;
using BMWStore.Models.FilterModels.BindingModels;
using BMWStore.Services.Interfaces;
using MappingRegistrar;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services
{
    public class NewCarsInvertoryService : INewCarsInvertoryService
    {
        private readonly IBMWStoreUnitOfWork unitOfWork;

        public NewCarsInvertoryService(IBMWStoreUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<NewCarsInvertoryViewModel> GetInvertoryBindingModel(
            ICarSortStrategy sortStrategy, 
            params ICarFilterStrategy[] filterStrategies)
        {
            var filteredCars = this.unitOfWork.NewCars
                .GetFiltered(filterStrategies);
            var sortedAndFilteredCars = sortStrategy.Sort(filteredCars);

            var carModels = await sortedAndFilteredCars.To<CarConciseViewModel>().ToArrayAsync();

            var yearModels = await sortedAndFilteredCars
                .GroupBy(c => c.Year)
                .Select(c => new FilterTypeBindingModel()
                {
                    CarsCount = c.Count(),
                    Value = c.Key,
                    Text = c.Key
                })
                .ToArrayAsync();

            var seriesModels = await sortedAndFilteredCars
                .GroupBy(c => new { Value = c.Series.Id, Text = c.Series.Name })
                .Select(c => new FilterTypeBindingModel()
                {
                    CarsCount = c.Count(),
                    Value = c.Key.Value,
                    Text = c.Key.Text
                })
                .ToArrayAsync();

            var modelTypeModels = await sortedAndFilteredCars
                .GroupBy(c => new { Value = c.ModelType.Id, Text = c.ModelType.Name })
                .Select(c => new FilterTypeBindingModel()
                {
                    CarsCount = c.Count(),
                    Value = c.Key.Value,
                    Text = c.Key.Text
                })
                .ToArrayAsync();

            var carType = new SqlParameter("type", "NewCar");

            var dataTable = new DataTable("BaseCars");
            dataTable.Columns.Add("Price");
            foreach (var car in carModels)
            {
                var row = dataTable.NewRow();
                row["Price"] = car.Price;
                dataTable.Rows.Add(row);
            }

            var cars = new SqlParameter("cars", SqlDbType.Structured);
            cars.TypeName = "[dbo].[BaseCars]";
            cars.Value = dataTable;
            var priceModels = await this.unitOfWork.Query<FilterTypeBindingModel>()
                .FromSql("EXECUTE usp_GetCarPriceRangesCount @cars=@cars", cars)
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
