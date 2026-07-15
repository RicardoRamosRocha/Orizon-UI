using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orizon.UI.TagHelpers;

[HtmlTargetElement("orizon-card")]
public sealed class OrizonCardTagHelper : TagHelper
{
    [HtmlAttributeName("class")]
    public string? CssClass { get; set; }

    public override void Process(
        TagHelperContext context,
        TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute(
            "class",
            BuildCssClass(CssClass));
    }

    private static string BuildCssClass(string? cssClass)
    {
        return string.IsNullOrWhiteSpace(cssClass)
            ? "orizon-card"
            : $"orizon-card {cssClass}";
    }
}