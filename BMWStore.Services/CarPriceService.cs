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
        private readonly IBMWStoreUnitOfWork unitOfWork;

        public CarPriceService(IBMWStoreUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<ICollection<FilterTypeBindingModel>> GetPriceFilterModels(IEnumerable<CarConciseViewModel> carModels)
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
    }
}
