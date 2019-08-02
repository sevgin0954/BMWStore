﻿using BMWStore.Models.OptionModels.BidningModels;
using BMWStore.Models.OptionModels.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices.Interfaces
{
    public interface IAdminOptionsService
    {
        Task CreateNewOptionAsync(AdminOptionCreateBindingModel model);
        Task<IEnumerable<OptionViewModel>> GetAllOptionsAsync();
        Task DeleteAsync(string carOptionId);
        Task<AdminCarOptionEditBindingModel> GetEditBindingModelAsync(string carOptionId);
        Task EditOptionAsync(AdminCarOptionEditBindingModel model);
    }
}
