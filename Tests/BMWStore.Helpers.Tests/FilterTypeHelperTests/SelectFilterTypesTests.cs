using BMWStore.Common.Constants;
using BMWStore.Models.FilterModels.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BMWStore.Helpers.Tests.FilterTypeHelperTests
{
    public class SelectFilterTypesTests
    {
        [Fact]
        public void WithNullFilterTypes_ShouldThrowException()
        {
            var value = Guid.NewGuid().ToString();

            var exception = Assert.Throws<ArgumentException>(() => FilterTypeHelper.SelectFilterTypes(null, value));
            Assert.Equal(ErrorConstants.CantBeNullParameter, exception.Message);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void WithNullOrEmptyValue_ShouldThrowException(string value)
        {
            var filterTypes = new List<FilterTypeBindingModel>();

            var exception = Assert.Throws<ArgumentException>(() => FilterTypeHelper.SelectFilterTypes(filterTypes, value));
            Assert.Equal(ErrorConstants.CantBeNullOrEmpty, exception.Message);
        }

        [Fact]
        public void WithNotExistingValue_ShouldNotSelectAnything()
        {
            var filterTypes = new List<FilterTypeBindingModel>();
            this.AddFilterModels(filterTypes);
            var value = Guid.NewGuid().ToString();

            FilterTypeHelper.SelectFilterTypes(filterTypes, value);

            Assert.True(filterTypes.All(ft => ft.IsSelected == false));
        }

        [Fact]
        public void WithExistingValue_ShouldSelectCorrectFilterType()
        {
            var filterTypes = new List<FilterTypeBindingModel>();
            var value = Guid.NewGuid().ToString();
            this.AddFilterModels(filterTypes, value);
            this.AddFilterModels(filterTypes);

            FilterTypeHelper.SelectFilterTypes(filterTypes, value);

            Assert.Single(filterTypes, ft => ft.IsSelected == true && ft.Value == value);
        }

        private void AddFilterModels(ICollection<FilterTypeBindingModel> models, string value = "")
        {
            var model = new FilterTypeBindingModel()
            {
                Value = value
            };
            models.Add(model);
        }
    }
}
