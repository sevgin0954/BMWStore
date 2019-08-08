using BMWStore.Data;
using BMWStore.Data.Interfaces;
using BMWStore.Models.CarModels.ViewModels;
using BMWStore.Models.FilterModels.BindingModels;
using BMWStore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services
{
    public class CarPriceService : ICarPriceService
    {
        private const string PriceColumnName = "Price";

        private readonly ApplicationDbContext dbContext;

        public CarPriceService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ICollection<FilterTypeBindingModel>> GetPriceFilterModelsAsync<TModel>(
            IEnumerable<TModel> carModels) where TModel : CarConciseViewModel
        {
            var dataTable = new DataTable("BaseCars");
            this.AddDataToDataTable(dataTable, carModels);

            var cars = new SqlParameter("cars", SqlDbType.Structured)
            {
                TypeName = "[dbo].[BaseCars]",
                Value = dataTable
            };
            var priceModels = await this.GetFilterModelsFromProcedureAsync(cars);

            return priceModels;
        }

        private void AddDataToDataTable(DataTable dataTable, IEnumerable<CarConciseViewModel> carModels)
        {
            dataTable.Columns.Add(PriceColumnName);
            foreach (var car in carModels)
            {
                var row = dataTable.NewRow();
                row[PriceColumnName] = car.Price;
                dataTable.Rows.Add(row);
            }
        }

        private async Task<ICollection<FilterTypeBindingModel>> GetFilterModelsFromProcedureAsync(SqlParameter cars)
        {
            var priceModels = await this.dbContext.Query<FilterTypeBindingModel>()
                .FromSql($"EXECUTE usp_GetCarPriceRangesCount @{cars.ParameterName}=@cars", cars)
                .Select(c => new FilterTypeBindingModel() { Value = c.Value, Text = $"{c.Text} ({c.CarsCount})" })
                .ToListAsync();

            return priceModels;
        }
    }
}
