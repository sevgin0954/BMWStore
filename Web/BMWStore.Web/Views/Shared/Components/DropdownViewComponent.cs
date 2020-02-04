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
            string prependText = "",
            string methodType = "post")
        {
            var enumType = Type.GetType(enumTypeName);

            DataValidator.ValidateEnumType(enumType);
            DataValidator.ValidateNotEmptyEnum(enumType, ErrorConstants.EmptyEnum);
            DataValidator.ValidateEnumValue(selectedEnumName, enumType);

            area = GetArea(area);
            controllerName = GetControllerName(controllerName);
            actionName = GetActionName(actionName);
            returnUrl = GetReturnUrl(returnUrl);

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
                PrependText = prependText,
                MethodType = methodType
            };

            return View(model);
        }
        private string GetArea(string area)
        {
            if (string.IsNullOrEmpty(area))
            {
                var defaultArea = this.ViewContext.RouteData.Values[AreaValue]?.ToString();
                area = defaultArea;
            }

            return area;
        }

        private string GetControllerName(string controllerName)
        {
            if (string.IsNullOrEmpty(controllerName))
            {
                var defaultController = this.ViewContext.RouteData.Values[ControllerValue].ToString();
                controllerName = defaultController;
            }

            return controllerName;
        }

        private string GetActionName(string actionName)
        {
            if (string.IsNullOrEmpty(actionName))
            {
                var defaultAction = this.ViewContext.RouteData.Values[ActionValue].ToString();
                actionName = defaultAction;
            }

            return actionName;
        }

        private string GetReturnUrl(string returnUrl)
        {
            if (returnUrl == null)
            {
                var currentFullPath =
                    this.ViewContext.HttpContext.Request.Path +
                    this.ViewContext.HttpContext.Request.QueryString;
                returnUrl = currentFullPath;
            }

            return returnUrl;
        }
    }
}
