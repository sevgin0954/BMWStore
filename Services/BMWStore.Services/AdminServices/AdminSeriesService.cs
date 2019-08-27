using AutoMapper;
using BMWStore.Common.Constants;
using BMWStore.Common.Validation;
using BMWStore.Data.Repositories.Extensions;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Models;
using MappingRegistrar;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices
{
    public class AdminSeriesService : IAdminSeriesService
    {
        private readonly ISeriesRepository seriesRepository;
        private readonly IAdminDeleteService adminDeleteService;
        private readonly IAdminEditService adminEditService;

        public AdminSeriesService(
            ISeriesRepository seriesRepository,
            IAdminDeleteService adminDeleteService,
            IAdminEditService adminEditService)
        {
            this.seriesRepository = seriesRepository;
            this.adminDeleteService = adminDeleteService;
            this.adminEditService = adminEditService;
        }

        public async Task CreateNewAsync(SeriesServiceModel model)
        {
            var dbSeries = Mapper.Map<Series>(model);
            this.seriesRepository.Add(dbSeries);

            var rowsAffected = await this.seriesRepository.CompleteAsync();
            RepositoryValidator.ValidateCompleteChanges(rowsAffected);
        }

        public async Task DeleteAsync(string seriesId)
        {
            await this.adminDeleteService.DeleteAsync<Series>(seriesId);
        }

        public IQueryable<SeriesServiceModel> GetAll()
        {
            var models = this.seriesRepository.GetAll().To<SeriesServiceModel>();

            return models;
        }

        public async Task<SeriesServiceModel> GetByIdAsync(string id)
        {
            var model = await this.seriesRepository
                .FindAll(id)
                .To<SeriesServiceModel>()
                .FirstOrDefaultAsync();
            DataValidator.ValidateNotNull(model, new ArgumentException(ErrorConstants.IncorrectId));

            return model;
        }

        public async Task EditAsync(SeriesServiceModel model)
        {
            await this.adminEditService.EditAsync<Series, SeriesServiceModel>(model, model.Id);
        }

    }
}