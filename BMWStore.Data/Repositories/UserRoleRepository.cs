using BMWStore.Data.Repositories.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BMWStore.Data.Repositories
{
    public class UserRoleRepository : BaseRepository<IdentityUserRole<string>>
    {
        public UserRoleRepository(DbContext dbContext)
            : base(dbContext) { }
    }
}
