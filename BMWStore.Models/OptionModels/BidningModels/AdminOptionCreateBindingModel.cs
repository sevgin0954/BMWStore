﻿using BMWStore.Common.Constants;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Models.OptionModels.BidningModels
{
    public class AdminOptionCreateBindingModel : IMapTo<Option>
    {
        [MaxLength(EntitiesConstants.OptionNameMaxLength)]
        [MinLength(EntitiesConstants.OptionNameMinLength)]
        [Required]
        public string Name { get; set; }

        [Range(typeof(decimal), EntitiesConstants.MinPrice, EntitiesConstants.OptionNameMaxPrice)]
        [Required]
        public decimal Price { get; set; }
    }
}