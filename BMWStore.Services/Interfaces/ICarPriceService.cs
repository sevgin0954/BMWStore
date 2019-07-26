﻿using BMWStore.Models.CarModels.ViewModels;
using BMWStore.Models.FilterModels.BindingModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.Interfaces
{
    public interface ICarPriceService
    {
        Task<ICollection<FilterTypeBindingModel>> GetPriceFilterModels(IEnumerable<CarConciseViewModel> carModels);
    }
}