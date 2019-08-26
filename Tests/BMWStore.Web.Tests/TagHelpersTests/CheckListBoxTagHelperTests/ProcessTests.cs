using BMWStore.Web.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BMWStore.Web.Tests.TagHelpersTests.CheckListBoxTagHelperTests
{
    public class ProcessTests
    {
        [Fact]
        public void WithSelectedItem_ShouldCreateInputForSelectedProperty()
        {
            var tagHelper = new CheckListBoxTagHelper();
            var modelName = Guid.NewGuid().ToString();
            var items = new List<SelectListItem>();
            var item = this.AddSelectListItem(items, true);

            var context = this.GetHelperContext();
            var output = this.GetHelperOutput();

            tagHelper.ModelName = modelName;
            tagHelper.Items = items;
            tagHelper.Process(context, output);

            var expectedSelectedInputHtml = 
                $"<input " +
                    $"type=\"checkbox\" " +
                    $"checked=\"checked\" " +
                    $"id=\"{modelName}_{0}__Selected\" " +
                    $"name=\"{modelName}[{0}].Selected\" " +
                    $"value=\"true\" />";

            Assert.Contains(expectedSelectedInputHtml, output.Content.GetContent());
        }

        [Fact]
        public void WithNotSelectedItem_ShouldCreateCorrectInputForSelectedProperty()
        {
            var tagHelper = new CheckListBoxTagHelper();
            var modelName = Guid.NewGuid().ToString();
            var items = new List<SelectListItem>();
            var item = this.AddSelectListItem(items, false);

            var context = this.GetHelperContext();
            var output = this.GetHelperOutput();

            tagHelper.ModelName = modelName;
            tagHelper.Items = items;
            tagHelper.Process(context, output);

            var expectedSelectedInputHtml =
                $"<input " +
                    $"type=\"checkbox\" " +
                    " " +
                    $"id=\"{modelName}_{0}__Selected\" " +
                    $"name=\"{modelName}[{0}].Selected\" " +
                    $"value=\"true\" />";

            Assert.Contains(expectedSelectedInputHtml, output.Content.GetContent());
        }

        private TagHelperContext GetHelperContext()
        {
            var tagHelperContext = new TagHelperContext(
                new TagHelperAttributeList(),
                new Dictionary<object, object>(),
                Guid.NewGuid().ToString("N"));

            return tagHelperContext;
        }

        private TagHelperOutput GetHelperOutput()
        {
            var tagHelperOutput = new TagHelperOutput("markdown",
                new TagHelperAttributeList(),
                (result, encoder) =>
                {
                    var tagHelperContent = new DefaultTagHelperContent();
                    tagHelperContent.SetHtmlContent(string.Empty);
                    return Task.FromResult<TagHelperContent>(tagHelperContent);
                });

            return tagHelperOutput;
        }

        private SelectListItem AddSelectListItem(ICollection<SelectListItem> items, bool isSelected)
        {
            var selectListItem = new SelectListItem()
            {
                Selected = isSelected,
                Text = Guid.NewGuid().ToString(),
                Value = Guid.NewGuid().ToString()
            };
            items.Add(selectListItem);

            return selectListItem;
        }
    }
}
