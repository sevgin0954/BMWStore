using BMWStore.Common.Constants;
using BMWStore.Common.Validation;
using BMWStore.Models.ViewComponentModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BMWStore.Web.Views.Shared.Components
{
    public class SortTypeViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string enumTypeName, string selectedEnumName)
        {
            var enumType = Type.GetType(enumTypeName);
            var enumData = Enum.GetNames(enumType);

            DataValidator.ValidateEnumValue(selectedEnumName, enumType);
            DataValidator.ValidateNotEmptyEnum(enumType, ErrorConstants.EmptyEnumException);

            var model = new SortTypeViewModel()
            {
                SelectedSortName = selectedEnumName,
                SortNames = enumData
            };

            return View(model);
        }
    }
}
