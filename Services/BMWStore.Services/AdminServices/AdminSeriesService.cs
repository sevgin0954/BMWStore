using AutoMapper;
using BMWStore.Common.Validation;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Interfaces;
using BMWStore.Services.Models;
using MappingRegistrar;
using System.Linq;
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

        public async Task<TModel> GetByIdAsync<TModel>(string id) where TModel : class
        {
            var model = await this.readService.GetModelByIdAsync<TModel, Series>(id);

            return model;
        }

        public async Task CreateNewAsync(SeriesServiceModel model)
        {
            var dbSeries = Mapper.Map<Series>(model);
            this.seriesRepository.Add(dbSeries);

            var rowsAffected = await this.seriesRepository.CompleteAsync();
            RepositoryValidator.ValidateCompleteChanges(rowsAffected);
        }

        public IQueryable<SeriesServiceModel> GetAll()
        {
            var models = this.seriesRepository.GetAll().To<SeriesServiceModel>();

            return models;
        }

        public async Task EditAsync(SeriesServiceModel model)
        {
            await this.adminEditService.EditAsync<Series, SeriesServiceModel>(model, model.Id);
        }

        public async Task DeleteAsync(string seriesId)
        {
            await this.adminDeleteService.DeleteAsync<Series>(seriesId);
        }
    }
}