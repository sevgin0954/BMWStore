using BMWStore.Models.FilterModels.BindingModels;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;

namespace BMWStore.Web.TagHelpers
{
    [HtmlTargetElement(Attributes = "for, items")]
    public class SelectFilterTypeHelper : TagHelper
    {
        [HtmlAttributeName("for")]
        public string InputName { get; set; }

        [HtmlAttributeName("items")]
        public IEnumerable<FilterTypeBindingModel> Items { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            foreach (var item in this.Items)
            {
                var selected = item.IsSelected ? "selected" : "";
                var text = $"{item.Text} ({item.CarsCount})";
                output.PostContent.AppendHtml($"<option {selected} value=\"{item.Value}\" >{text}</option>");
            }
        }
    }
}
