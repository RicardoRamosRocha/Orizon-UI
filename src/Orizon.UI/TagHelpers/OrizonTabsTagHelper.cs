using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orizon.UI.TagHelpers;

[HtmlTargetElement("orizon-tabs")]
public sealed class OrizonTabsTagHelper : TagHelper
{
    [HtmlAttributeName("label")]
    public string Label { get; set; } = "Abas";

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var content = await output.GetChildContentAsync();
        var existingClass = output.Attributes["class"]?.Value?.ToString();

        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;
        output.Attributes.RemoveAll("class");
        output.Attributes.SetAttribute("class", TagHelperClassBuilder.Build("orizon-tabs", existingClass));
        output.Attributes.SetAttribute("data-orizon-tabs", "");
        output.Attributes.SetAttribute("aria-label", Label);
        output.Content.SetHtmlContent(content.GetContent());
    }
}
