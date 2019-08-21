using BMWStore.Data;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
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

        public async Task<ICollection<FilterTypeBindingModel>> GetPriceFilterModelsAsync(IQueryable<BaseCar> cars)
        {
            var dataTable = new DataTable("BaseCars");
            var carPrices = await cars.Select(c => c.Price).ToArrayAsync();
            this.AddDataToDataTable(dataTable, carPrices);

            var pricesParameters = new SqlParameter("cars", SqlDbType.Structured)
            {
                TypeName = "[dbo].[BaseCars]",
                Value = dataTable
            };
            var priceModels = await this.GetFilterModelsFromProcedureAsync(pricesParameters);

            return priceModels;
        }

        private void AddDataToDataTable(DataTable dataTable, IEnumerable<decimal> prices)
        {
            dataTable.Columns.Add(PriceColumnName);
            foreach (var price in prices)
            {
                var row = dataTable.NewRow();
                row[PriceColumnName] = price;
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
