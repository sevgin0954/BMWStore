using BMWStore.Common.Constants;
using BMWStore.Models.ViewComponentModels.ViewModels;
using BMWStore.Web.Views.Shared.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Routing;
using System;
using Xunit;

namespace BMWStore.Web.Tests.ViewComponentsTests.DropdownViewComponentTests
{
    public class InvokeTests
    {
        [Fact]
        public void WithNotEnumTypeName_ShouldThrowException()
        {
            var viewComponent = new DropdownViewComponent();
            var typeName = typeof(InvokeTests).AssemblyQualifiedName;

            var exception = Assert.Throws<ArgumentException>(() => viewComponent
                .Invoke(typeName, null, null, null, null, null, null));
            Assert.Equal(ErrorConstants.TypeWasNotEnum, exception.Message);
        }

        [Fact]
        public void WithEmptyEnum_ShouldThrowException()
        {
            var viewComponent = new DropdownViewComponent();
            var typeName = typeof(EmptyEnum).AssemblyQualifiedName;

            var exception = Assert.Throws<InvalidOperationException>(() => viewComponent
                .Invoke(typeName, null, null, null, null, null, null));
            Assert.Equal(ErrorConstants.EmptyEnum, exception.Message);
        }

        [Fact]
        public void WithIncorrectEnumTypeName_ShouldThrowException()
        {
            var viewComponent = new DropdownViewComponent();

            var typeName = typeof(TestEnum).AssemblyQualifiedName;
            var selectedEnumName = "c";

            var exception = Assert.Throws<ArgumentException>(() => viewComponent
                .Invoke(typeName, selectedEnumName, null, null, null, null, null));
            Assert.Equal(ErrorConstants.IncorrectEnumValue, exception.Message);
        }

        [Fact]
        public void WithViewWithoutArea_ShouldSetAreaToNull()
        {
            var viewComponent = new DropdownViewComponent();

            var typeName = typeof(TestEnum).AssemblyQualifiedName;
            var selectedEnumName = TestEnum.a.ToString();

            var httpContext = this.GetHttpContext();
            var viewContext = this.GetViewContext(httpContext);
            this.AddComponentContext(viewComponent, viewContext);

            var resultModel = this.InvokeComponent(viewComponent, typeName, selectedEnumName, areaName: null);

            Assert.Null(resultModel.AreaName);
        }

        [Fact]
        public void WithoutRouteValues_ShouldSetDefaultRouteValues()
        {
            var viewComponent = new DropdownViewComponent();

            var defaultArea = Guid.NewGuid().ToString();
            var defaultController = Guid.NewGuid().ToString();
            var defaultAction = Guid.NewGuid().ToString();

            var httpContext = this.GetHttpContext();
            var viewContext = this.GetViewContextWithArea(httpContext, defaultArea, defaultController, defaultAction);
            this.AddComponentContext(viewComponent, viewContext);

            var typeName = typeof(TestEnum).AssemblyQualifiedName;
            var selectedEnumName = TestEnum.a.ToString();

            var resultModel = this.InvokeComponent(viewComponent, typeName, selectedEnumName, null, null, null, null, null, null);

            var expectedReuturnUrl = httpContext.Request.Path + httpContext.Request.QueryString;

            Assert.Equal(defaultArea, resultModel.AreaName);
            Assert.Equal(defaultController, resultModel.ControllerName);
            Assert.Equal(defaultAction, resultModel.ActionName);
            Assert.Equal(expectedReuturnUrl, resultModel.ReturnUrl);
        }

        [Fact]
        public void WithRouteValues_ShouldSetRouteValues()
        {
            var viewComponent = new DropdownViewComponent();

            var area = Guid.NewGuid().ToString();
            var controller = Guid.NewGuid().ToString();
            var action = Guid.NewGuid().ToString();
            var returnUrl = Guid.NewGuid().ToString();

            var typeName = typeof(TestEnum).AssemblyQualifiedName;
            var selectedEnumName = TestEnum.a.ToString();

            var resultModel = this.InvokeComponent(
                viewComponent, 
                typeName, 
                selectedEnumName,
                areaName: area,
                controllerName: controller,
                actionName: action,
                returnUrl: returnUrl);

            Assert.Equal(area, resultModel.AreaName);
            Assert.Equal(controller, resultModel.ControllerName);
            Assert.Equal(action, resultModel.ActionName);
            Assert.Equal(returnUrl, resultModel.ReturnUrl);
        }

        [Fact]
        public void WithPrependText_ShouldSetPrependText()
        {
            var viewComponent = new DropdownViewComponent();

            var prependText = Guid.NewGuid().ToString();

            var typeName = typeof(TestEnum).AssemblyQualifiedName;
            var selectedEnumName = TestEnum.a.ToString();

            var resultModel = this.InvokeComponent(viewComponent, typeName, selectedEnumName, prependText: prependText);

            Assert.Equal(prependText, resultModel.PrependText);
        }

        private ViewContext GetViewContextWithArea(
            HttpContext httpContext, 
            string areaName, 
            string defaultController = "",
            string defaultAction = "")
        {
            var viewContext = this.GetViewContext(httpContext, defaultController, defaultAction);
            viewContext.RouteData.Values["area"] = areaName;

            return viewContext;
        }

        private ViewContext GetViewContext(
            HttpContext httpContext,
            string defaultController = "", 
            string defaultAction = "")
        {
            var viewContext = new ViewContext();
            viewContext.HttpContext = httpContext;
            viewContext.RouteData = new RouteData();
            viewContext.RouteData.Values["controller"] = defaultController;
            viewContext.RouteData.Values["action"] = defaultAction;

            return viewContext;
        }

        private DropdownViewModel InvokeComponent(
            DropdownViewComponent viewComponent,
            string enumTypeName,
            string selectedEnumName,
            string areaName = "default",
            string controllerName = "default",
            string actionName = "default",
            string routeParamName = "default",
            string returnUrl = "/",
            string prependText = "default")
        {
            var result = viewComponent.Invoke(
                enumTypeName,
                selectedEnumName,
                areaName,
                controllerName,
                actionName,
                routeParamName,
                returnUrl,
                prependText) as ViewViewComponentResult;
            var resultModel = result.ViewData.Model as DropdownViewModel;

            return resultModel;
        }

        private HttpContext GetHttpContext()
        {
            var requestPath = "/" + Guid.NewGuid().ToString();
            var queryValue = Guid.NewGuid().ToString();
            var queryKey = Guid.NewGuid().ToString();

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Path = requestPath;
            httpContext.Request.QueryString = QueryString.Create(queryValue, queryKey);

            return httpContext;
        }

        private void AddComponentContext(DropdownViewComponent viewComponent, ViewContext viewContext)
        {
            var context = new ViewComponentContext();
            context.ViewContext = viewContext;
            viewComponent.ViewComponentContext = context;
        }
    }

    public enum TestEnum
    {
        a, b
    }

    public enum EmptyEnum
    {

    }
}
