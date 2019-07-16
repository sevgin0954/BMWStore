using AutoMapper;
using BMWStore.Common.Validation;
using BMWStore.Data.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.SeriesModels.BindingModels;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services
{
    public class SeriesService : ISeriesService
    {
        private readonly IBMWStoreUnitOfWork unitOfWork;

        public SeriesService(IBMWStoreUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateNewSeriesAsync(AdminSeriesCreateBindingModel model)
        {
            var dbSeries = Mapper.Map<Series>(model);
            this.unitOfWork.Series.Add(dbSeries);

            var rowsAffected = await this.unitOfWork.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }

        public async Task<IEnumerable<SelectListItem>> GetAllAsSelectListItemsAsync()
        {
            var dbSeries = await this.unitOfWork.Series.GetAllAsync();
            var selectListItems = Mapper.Map<IEnumerable<SelectListItem>>(dbSeries);

            return selectListItems;
        }
    }
}
