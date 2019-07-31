using BMWStore.Data.Repositories.Generic;
using BMWStore.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Data.Repositories
{
    public class RoleRepository : BaseRepository<IdentityRole>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }

        public async Task<string> GetIdByNameAsync(string name)
        {
            var dbRole = await this.GetAll()
                .Where(r => r.NormalizedName == name.ToUpper())
                .Select(r => new { r.Id })
                .FirstAsync();

            return dbRole.Id;
        }
    }
}
