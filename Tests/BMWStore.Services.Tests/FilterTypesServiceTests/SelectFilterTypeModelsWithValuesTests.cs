using BMWStore.Models.FilterModels.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BMWStore.Services.Tests.FilterTypesServiceTests
{
    public class SelectFilterTypeModelsWithValuesTests : BaseFilterTypesServiceTest
    {
        [Fact]
        public void WithNullFilterTypeBindingModels_ShouldThrowException()
        {
            var service = this.GetService();
            var values = Guid.NewGuid().ToString();

            Assert.Throws<NullReferenceException>(() => service.SelectFilterTypeModelsWithValues(null, values));
        }

        [Fact]
        public void WithNulValues_ShouldThrowExcpetion()
        {
            var service = this.GetService();
            var model = this.GetModels(1);

            Assert.Throws<NullReferenceException>(() => service.SelectFilterTypeModelsWithValues(model, null));
        }

        [Fact]
        public void WithoutValues_ShouldNotSelectModel()
        {
            var service = this.GetService();
            var model = this.GetModels(1);

            service.SelectFilterTypeModelsWithValues(model);

            Assert.True(model.All(m => m.IsSelected == false));
        }

        [Fact]
        public void WithNotMachingValues_ShouldNotSelectModel()
        {
            var service = this.GetService();
            var model = this.GetModels(2);
            var value = Guid.NewGuid().ToString();

            service.SelectFilterTypeModelsWithValues(model, value);

            Assert.True(model.All(m => m.IsSelected == false));
        }

        [Fact]
        public void WithMachingValues_ShouldCheckModel()
        {
            var service = this.GetService();
            var model = this.GetModels(2);
            var value = model.First().Value;

            service.SelectFilterTypeModelsWithValues(model, value);

            Assert.Single(model.Where(m => m.IsSelected && m.Value == value));
        }

        private IEnumerable<FilterTypeBindingModel> GetModels(int count)
        {
            var models = new List<FilterTypeBindingModel>();

            for (int i = 0; i < count; i++)
            {
                var model = new FilterTypeBindingModel()
                {
                    Value = Guid.NewGuid().ToString()
                };
                models.Add(model);
            }

            return models;
        }
    }
}
