using BMWStore.Data.Repositories.Generic;
using BMWStore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Data.Repositories
{
    public class CarOptionRepository : BaseRepository<CarOption>
    {
        public CarOptionRepository(DbContext dbContext)
            : base(dbContext) { }

        public async Task RemoveWithCarIdAsync(string carId)
        {
            var carsOptions = await this.Find(co => co.CarId == carId)
                .ToArrayAsync();

            this.RemoveRange(carsOptions);
        }
    }
}
