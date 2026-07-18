using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orizon.UI.TagHelpers;

[HtmlTargetElement("orizon-toolbar")]
public sealed class OrizonToolbarTagHelper : TagHelper
{
    [HtmlAttributeName("label")] public string Label { get; set; } = "Ações da página";
    [HtmlAttributeName("sticky")] public bool Sticky { get; set; }
    [HtmlAttributeName("wrap")] public bool Wrap { get; set; } = true;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var content = await output.GetChildContentAsync(); var cls = output.Attributes["class"]?.Value?.ToString(); output.Attributes.RemoveAll("class");
        output.TagName = "div"; output.TagMode = TagMode.StartTagAndEndTag;
        output.Attributes.SetAttribute("class", TagHelperClassBuilder.Build("orizon-toolbar", Sticky ? "orizon-toolbar--sticky" : null, Wrap ? "orizon-toolbar--wrap" : null, cls));
        output.Attributes.SetAttribute("role", "toolbar"); output.Attributes.SetAttribute("aria-label", Label); output.Attributes.SetAttribute("data-orizon-toolbar", ""); output.Content.SetHtmlContent(content);
    }
}
