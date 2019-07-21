using BMWStore.Data.Repositories.Generic;
using BMWStore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Data.Repositories
{
    public class PictureRepository : BaseRepository<Picture>
    {
        public PictureRepository(DbContext dbContext)
            : base(dbContext) { }

        public async Task RemoveWithCarIdAsync(string carId)
        {
            var dbOptions = await this.GetAll()
                .Where(o => o.CarId == carId)
                .ToArrayAsync();

            this.RemoveRange(dbOptions);
        }
    }
}
