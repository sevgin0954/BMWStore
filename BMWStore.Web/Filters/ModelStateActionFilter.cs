using BMWStore.Common.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace BMWStore.Web.Filters
{
    public class ModelStateActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context) { }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid == false)
            {
                var controller = context.Controller as Controller;
                this.AddStatusMessage(context.ModelState, controller.TempData);

                context.Result = controller.RedirectToAction();
            }
        }

        private void AddStatusMessage(ModelStateDictionary modelState, ITempDataDictionary tempData)
        {
            int errorId = 0;

            foreach (var value in modelState.Values)
            {
                foreach (var error in value.Errors)
                {
                    tempData[WebConstants.StatusMessagePrefix + errorId] = error.ErrorMessage;
                    errorId++;
                }
            }
        }
    }
}
