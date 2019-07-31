using BMWStore.Data.Repositories.Generic;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BMWStore.Data.Repositories
{
    public class CarOptionRepository : BaseRepository<CarOption>, ICarOptionRepository
    {
        public CarOptionRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }

        public async Task RemoveAllWithCarIdAsync(string carId)
        {
            var carsOptions = await this.Find(co => co.CarId == carId)
                .ToArrayAsync();

            this.RemoveRange(carsOptions);
        }
    }
}
