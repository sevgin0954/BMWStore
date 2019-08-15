using AutoMapper;
using BMWStore.Common.Validation;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.SeriesModels.BindingModels;
using BMWStore.Models.SeriesModels.ViewModels;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices
{
    public class AdminSeriesService : IAdminSeriesService
    {
        private readonly ISeriesRepository seriesRepository;
        private readonly IReadService readService;
        private readonly IAdminDeleteService adminDeleteService;

        public AdminSeriesService(
            ISeriesRepository seriesRepository, 
            IReadService readService, 
            IAdminDeleteService adminDeleteService)
        {
            this.seriesRepository = seriesRepository;
            this.readService = readService;
            this.adminDeleteService = adminDeleteService;
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
            var models = await this.readService.GetAllAsync<SeriesViewModel, Series>();

            return models;
        }

        public async Task DeleteAsync(string seriesId)
        {
            await this.adminDeleteService.DeleteAsync<Series>(seriesId);
        }
    }
}
