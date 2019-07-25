using BMWStore.Common.Constants;
using BMWStore.Data.FilterStrategies.CarStrategies.Interfaces;
using BMWStore.Data.Interfaces;
using BMWStore.Data.SortStrategies.CarsStrategies.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.CarInvertoryModels.ViewModels;
using BMWStore.Models.CarModels.ViewModels;
using BMWStore.Models.FilterModels.BindingModels;
using BMWStore.Services.Interfaces;
using MappingRegistrar;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

            // TODO: Use caching
            var yearModels = await this.GetYearModel(sortedAndFilteredCars);
            var seriesModels = await this.GetSeriesModels(sortedAndFilteredCars);
            var modelTypeModels = await this.GetModelTypeModels(sortedAndFilteredCars);
            var priceModels = await this.GetPriceModel(carModels);

            var model = new NewCarsInvertoryViewModel();

            var allFilterModel = this.AddAllFilterTypeModel();
            model.Years.Add(allFilterModel);
            model.Series.Add(allFilterModel);
            model.Prices.Add(allFilterModel);

            model.Years.AddRange(yearModels);
            model.Series.AddRange(seriesModels);
            model.ModelTypes.AddRange(modelTypeModels);
            model.Prices.AddRange(priceModels);

            model.Cars = carModels;

            return model;
        }

        private async Task<ICollection<FilterTypeBindingModel>> GetYearModel(IQueryable<BaseCar> cars)
        {
            var yearModels = await cars
                .GroupBy(c => c.Year)
                .Select(c => new FilterTypeBindingModel()
                {
                    Value = c.Key,
                    Text = $"{c.Key} ({c.Count()})"
                })
                .ToListAsync();

            return yearModels;
        }

        private async Task<ICollection<FilterTypeBindingModel>> GetSeriesModels(IQueryable<BaseCar> cars)
        {
            var seriesModels = await cars
                .GroupBy(c => new { Value = c.Series.Id, Text = c.Series.Name })
                .Select(c => new FilterTypeBindingModel()
                {
                    Value = c.Key.Value,
                    Text = $"{c.Key.Text} ({c.Count()})"
                })
                .ToListAsync();

            return seriesModels;
        }

        private async Task<ICollection<FilterTypeBindingModel>> GetModelTypeModels(IQueryable<BaseCar> cars)
        {
            var modelTypeModels = await cars
                .GroupBy(c => new { Value = c.ModelType.Id, Text = c.ModelType.Name })
                .Select(c => new FilterTypeBindingModel()
                {
                    Value = c.Key.Value,
                    Text = $"{c.Key.Text} ({c.Count()})"
                })
                .ToListAsync();

            return modelTypeModels;
        }

        private async Task<ICollection<FilterTypeBindingModel>> GetPriceModel(IEnumerable<CarConciseViewModel> carModels)
        {
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
                .Select(c => new FilterTypeBindingModel() { Value = c.Value, Text = $"{c.Text} ({c.CarsCount})" })
                .ToListAsync();

            return priceModels;
        }

        private FilterTypeBindingModel AddAllFilterTypeModel()
        {
            var allModel = new FilterTypeBindingModel()
            {
                Text = WebConstants.AllFilterTypeModelText,
                Value = WebConstants.AllFilterTypeModelValue
            };

            return allModel;
        }
    }
}
