using BMWStore.Common.Constants;
using BMWStore.Common.Validation;
using BMWStore.Models.ViewComponentModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BMWStore.Web.Views.Shared.Components
{
    public class DropdownViewComponent : ViewComponent
    {
        private const string AreaValue = "area";
        private const string ControllerValue = "controller";
        private const string ActionValue = "action";

        public IViewComponentResult Invoke(
            string enumTypeName, 
            string selectedEnumName, 
            string area,
            string controllerName, 
            string actionName,
            string routeParamName,
            string returnUrl,
            string prependText = "")
        {
            var enumType = Type.GetType(enumTypeName);

            DataValidator.ValidateNotEmptyEnum(enumType, ErrorConstants.EmptyEnum);
            DataValidator.ValidateEnumValue(selectedEnumName, enumType);

            if (string.IsNullOrEmpty(area))
            {
                var defaultArea = this.ViewContext.RouteData.Values[AreaValue].ToString();
                area = defaultArea;
            }
            if (string.IsNullOrEmpty(controllerName))
            {
                var defaultController = this.ViewContext.RouteData.Values[ControllerValue].ToString();
                controllerName = defaultController;
            }
            if (string.IsNullOrEmpty(actionName))
            {
                var defaultAction = this.ViewContext.RouteData.Values[ActionValue].ToString();
                actionName = defaultAction;
            }
            if (returnUrl == null)
            {
                var currentFullPath = 
                    this.ViewContext.HttpContext.Request.Path + 
                    this.ViewContext.HttpContext.Request.QueryString;
                returnUrl = currentFullPath;
            }

            var enumNames = Enum.GetNames(enumType);
            var model = new DropdownViewModel()
            {
                SelectedSortName = selectedEnumName,
                SortNames = enumNames,
                AreaName = area,
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
