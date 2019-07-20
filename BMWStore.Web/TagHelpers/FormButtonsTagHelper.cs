using BMWStore.Common.Constants;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.IO;
using System.Text.Encodings.Web;

namespace BMWStore.Web.TagHelpers
{
    [HtmlTargetElement("form-buttons")]
    public class FormButtonsTagHelper : TagHelper
    {
        private readonly IHtmlGenerator generator;

        public FormButtonsTagHelper(IHtmlGenerator generator)
        {
            this.generator = generator;
        }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";

            var cancelAnchorTag = this.CreateAnchorTag(
                WebConstants.FormButtonsCancelLinkText, 
                "btn border-light-red background-light-red-hover");
            var resultHtml =
                "<div class=\"d-flex mt-4\">" +
                    "<div class=\"mx-auto\">" +
                        "<button class=\"btn border-green background-green-hover mr-3\" type=\"submit\">Submit</button>" +
                        cancelAnchorTag +
                    "</div>" +
                "</div>";

            output.Content.SetHtmlContent(resultHtml);
        }

        private string CreateAnchorTag(string text, string cssClass)
        {
            var tagBuilder = this.generator.GenerateActionLink(
                ViewContext,
                linkText: text,
                actionName: this.ActionName,
                controllerName: this.ControllerName,
                protocol: null,
                hostname: null,
                fragment: null,
                routeValues: null,
                htmlAttributes: null);
            tagBuilder.AddCssClass(cssClass);

            var result = this.GetTagBuilderAsString(tagBuilder);
            return result;
        }

        private string GetTagBuilderAsString(TagBuilder tagBuilder)
        {
            using (var writer = new StringWriter())
            {
                tagBuilder.WriteTo(writer, HtmlEncoder.Default);
                var htmlOutput = writer.ToString();

                return htmlOutput;
            }
        }
    }
}