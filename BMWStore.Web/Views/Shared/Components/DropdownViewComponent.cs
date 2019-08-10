using BMWStore.Common.Constants;
using BMWStore.Common.Validation;
using BMWStore.Models.ViewComponentModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BMWStore.Web.Views.Shared.Components
{
    public class DropdownViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(
            string enumTypeName, 
            string selectedEnumName, 
            string controllerName, 
            string actionName,
            string routeParamName,
            string returnUrl,
            string prependText = "")
        {
            var enumType = Type.GetType(enumTypeName);
            var enumData = Enum.GetNames(enumType);

            DataValidator.ValidateEnumValue(selectedEnumName, enumType);
            DataValidator.ValidateNotEmptyEnum(enumType, ErrorConstants.EmptyEnum);

            if (returnUrl == null)
            {
                var fullPath = this.ViewContext.HttpContext.Request.Path + this.ViewContext.HttpContext.Request.QueryString;
                returnUrl = fullPath;
            }

            var model = new DropdownViewModel()
            {
                SelectedSortName = selectedEnumName,
                SortNames = enumData,
                ControllerName = controllerName,
                ActionName = actionName,
                ReturnUrl = returnUrl,
                ParameterName = routeParamName,
                PrependText = prependText
            };

            return View(model);
        }
    }
}
