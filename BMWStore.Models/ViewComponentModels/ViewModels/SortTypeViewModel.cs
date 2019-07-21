﻿using System;
using System.Collections.Generic;

namespace BMWStore.Models.ViewComponentModels.ViewModels
{
    public class SortTypeViewModel
    {
        public IEnumerable<string> SortNames { get; set; }

        public string SelectedSortName { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }
    }
}