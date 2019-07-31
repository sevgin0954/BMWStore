using BMWStore.Data.Repositories.Generic;
using BMWStore.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BMWStore.Data.Repositories
{
    public class UserRoleRepository : BaseRepository<IdentityUserRole<string>>, IUserRoleRepository
    {
        public UserRoleRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }
    }
}
