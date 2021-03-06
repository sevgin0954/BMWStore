﻿using BMWStore.Common.Constants;
using System.Collections.Generic;

namespace BMWStore.Models.CarInventoryModels.BindingModels
{
    public class CarsInventoryBindingModel
    {
        public IEnumerable<string> ModelTypes { get; set; } = new List<string>();
        public string Year { get; set; } = WebConstants.AllFilterTypeModelValue;
        public string PriceRange { get; set; } = WebConstants.AllFilterTypeModelValue;
        public string Series { get; set; } = WebConstants.AllFilterTypeModelValue;
        public int PageNumber { get; set; } = 1;
    }
}
