using BMWStore.Data.Repositories.Generic;
using BMWStore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BMWStore.Data.Repositories
{
    public class CarRepository : BaseRepository<BaseCar>
    {
        public CarRepository(DbContext dbContext)
            : base(dbContext) { }

        public async Task<bool> IsType(Type type, string carId)
        {
            var isType = await this.GetAll().AnyAsync(c => c is NewCar && c.Id == carId);

            return isType;
        }
    }
}
