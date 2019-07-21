using BMWStore.Data.Repositories.Generic;
using BMWStore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Data.Repositories
{
    public class OptionRepository : BaseRepository<Option>
    {
        public OptionRepository(DbContext dbContext)
            : base(dbContext) { }
    }
}
