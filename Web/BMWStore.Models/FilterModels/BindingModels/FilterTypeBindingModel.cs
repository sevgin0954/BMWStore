﻿using BMWStore.Services.Models;
using MappingRegistrar.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMWStore.Models.FilterModels.BindingModels
{
    public class FilterTypeBindingModel : IMapFrom<FilterTypeServiceModel>, IMapTo<FilterTypeBindingModel>
    {
        public int CarsCount { get; set; }

        [NotMapped]
        public bool IsSelected { get; set; }

        public string Value { get; set; }

        public string Text { get; set; }
    }
}
