﻿using BMWStore.Data.Repositories.Generic;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }

        public async Task<User> GetByEmailOrDefault(string email)
        {
            var user = await this.GetAll()
                .Where(u => u.NormalizedEmail == email.ToUpper())
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<User> GetByIdWithRolesAsync(string userId)
        {
            var dbUser = await this.GetAll()
                .Include(u => u.Roles)
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            return dbUser;
        }
    }
}