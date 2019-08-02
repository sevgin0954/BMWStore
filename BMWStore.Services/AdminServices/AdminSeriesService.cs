using AutoMapper;
using BMWStore.Common.Constants;
using BMWStore.Common.Validation;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.SeriesModels.BindingModels;
using BMWStore.Models.SeriesModels.ViewModels;
using BMWStore.Services.AdminServices.Interfaces;
using MappingRegistrar;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices
{
    public class AdminSeriesService : IAdminSeriesService
    {
        private readonly ISeriesRepository seriesRepository;

        public AdminSeriesService(ISeriesRepository seriesRepository)
        {
            this.seriesRepository = seriesRepository;
        }

        public async Task CreateNewSeriesAsync(AdminSeriesCreateBindingModel model)
        {
            var dbSeries = Mapper.Map<Series>(model);
            this.seriesRepository.Add(dbSeries);

            var rowsAffected = await this.seriesRepository.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }

        public async Task<IEnumerable<SeriesViewModel>> GetAllAsync()
        {
            var models = await this.seriesRepository
                .GetAll()
                .To<SeriesViewModel>()
                .ToArrayAsync();

            return models;
        }

        public async Task<IEnumerable<SelectListItem>> GetAllAsSelectListItemsAsync()
        {
            var selectListItems = await this.seriesRepository
                .GetAll()
                .To<SelectListItem>()
                .ToArrayAsync();

            return selectListItems;
        }

        public async Task DeleteAsync(string seriesId)
        {
            var dbSeries = await this.seriesRepository.GetByIdAsync(seriesId);
            DataValidator.ValidateNotNull(dbSeries, new ArgumentException(ErrorConstants.IncorrectId));

            this.seriesRepository.Remove(dbSeries);

            var rowsAffected = await this.seriesRepository.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }
    }
}
