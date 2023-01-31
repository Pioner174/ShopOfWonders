using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SOW.ShopOfWonders.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("p", Attributes="asp-for")]
    public class ModelPTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-for")]
        public ModelExpression Model { get; set; }
        
        public override async void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName= "p";
            var content = Model.Model == null ? string.Empty : Model.Model.ToString();
            output.Content.SetHtmlContent(content);

            TagBuilder input = new TagBuilder("input");
            input.Attributes["type"] = "hidden";
            input.Attributes["id"] = Model.Name;
            input.Attributes["name"] = Model.Name;
            output.PostElement.AppendHtml(input);
        }

    }
}
