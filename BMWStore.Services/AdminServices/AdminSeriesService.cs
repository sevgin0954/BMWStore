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
        private readonly IAdminEditService adminEditService;

        public AdminSeriesService(
            ISeriesRepository seriesRepository, 
            IReadService readService, 
            IAdminDeleteService adminDeleteService,
            IAdminEditService adminEditService)
        {
            this.seriesRepository = seriesRepository;
            this.readService = readService;
            this.adminDeleteService = adminDeleteService;
            this.adminEditService = adminEditService;
        }

        public async Task CreateNewSeriesAsync(SeriesCreateBindingModel model)
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

        public async Task<SeriesEditBindingModel> GetEditingModelAsync(string seriesId)
        {
            var model = await this.readService.GetModelByIdAsync<SeriesEditBindingModel, Series>(seriesId);

            return model;
        }

        public async Task EditAsync(SeriesEditBindingModel model)
        {
            await this.adminEditService.EditAsync<Series, SeriesEditBindingModel>(model, model.Id);
        }

        public async Task DeleteAsync(string seriesId)
        {
            await this.adminDeleteService.DeleteAsync<Series>(seriesId);
        }
    }
}
