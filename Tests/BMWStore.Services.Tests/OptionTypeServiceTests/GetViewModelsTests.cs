using BMWStore.Models.OptionModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BMWStore.Services.Tests.OptionTypeServiceTests
{
    public class GetViewModelsTests : BaseOptionTypeServiceTest
    {
        [Fact]
        public void WithoutOptions_ShouldReturnEmptyCollection()
        {
            var service = this.GetService();
            var options = new List<OptionConciseViewModel>();

            var models = service.GetViewModels(options);

            Assert.Empty(models);
        }

        [Fact]
        public void WithOptionWithDifferentTypes_ShouldReturnCorrectModels()
        {
            var service = this.GetService();
            var options = new List<OptionConciseViewModel>();
            var option1 = this.AddOption(options, Guid.NewGuid().ToString());
            var option2 = this.AddOption(options, Guid.NewGuid().ToString());

            var models = service.GetViewModels(options);

            Assert.Equal(2, models.Count());
            Assert.Contains(models, m => m.Name == option1.OptionTypeName);
            Assert.Contains(models, m => m.Name == option2.OptionTypeName);
        }

        private OptionConciseViewModel AddOption(ICollection<OptionConciseViewModel> options, string optionTypeName)
        {
            var optionModel = new OptionConciseViewModel()
            {
                OptionTypeName = optionTypeName
            };
            options.Add(optionModel);

            return optionModel;
        }
    }
}
