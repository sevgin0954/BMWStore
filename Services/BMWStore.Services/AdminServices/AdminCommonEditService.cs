﻿using AutoMapper;
using BMWStore.Common.Constants;
using BMWStore.Common.Validation;
using BMWStore.Data;
using BMWStore.Services.AdminServices.Interfaces;
using System;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices
{
    public class AdminCommonEditService : IAdminCommonEditService
    {
        private readonly ApplicationDbContext dbContext;

        public AdminCommonEditService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task EditAsync<TEntity, TModel>(TModel editingModel, string id)
            where TEntity : class
            where TModel : class
        {
            var dbEntity = await this.dbContext.FindAsync<TEntity>(id);
            DataValidator.ValidateNotNull(dbEntity, new ArgumentException(ErrorConstants.IncorrectId));

            Mapper.Map(editingModel, dbEntity);

            var rowsAffected = await this.dbContext.SaveChangesAsync();
            RepositoryValidator.ValidateCompleteChanges(rowsAffected);
        }
    }
}
