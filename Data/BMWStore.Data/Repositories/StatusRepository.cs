using BMWStore.Data.Repositories.Generic;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Data.Repositories
{
    public class StatusRepository : BaseRepository<Status>, IStatusRepository
    {
        public StatusRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }

        public async Task<string> GetIdByNameAsync(string name)
        {
            var id = await this.GetAll()
                .Where(s => s.Name == name)
                .Select(s => s.Id)
                .FirstAsync();

            return id;
        }
    }
}
