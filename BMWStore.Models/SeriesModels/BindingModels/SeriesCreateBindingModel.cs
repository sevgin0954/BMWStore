﻿using BMWStore.Common.Constants;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Models.SeriesModels.BindingModels
{
    public class SeriesCreateBindingModel : IMapTo<Series>
    {
        [MaxLength(EntitiesConstants.SeriesNameMaxLength)]
        [MinLength(EntitiesConstants.SeriesNameMinLength)]
        [Required]
        public string Name { get; set; }
    }
}