using AutoMapper;
using BMWStore.Common.Validation;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.SeriesModels.BindingModels;
using BMWStore.Models.SeriesModels.ViewModels;
using BMWStore.Services.AdminServices.Interfaces;
using MappingRegistrar;
using Microsoft.EntityFrameworkCore;
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
    }
}
