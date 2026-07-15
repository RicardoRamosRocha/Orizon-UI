using Microsoft.AspNetCore.Razor.TagHelpers;
using Orizon.UI.Icons;

namespace Orizon.UI.TagHelpers;

[HtmlTargetElement("orizon-icon")]
public sealed class OrizonIconTagHelper : TagHelper
{
    [HtmlAttributeName("name")]
    public string Name { get; set; } = string.Empty;

    [HtmlAttributeName("size")]
    public int Size { get; set; } = 20;

    [HtmlAttributeName("stroke-width")]
    public decimal StrokeWidth { get; set; } = 1.8m;

    [HtmlAttributeName("label")]
    public string? Label { get; set; }

    [HtmlAttributeName("class")]
    public string? CssClass { get; set; }

    public override void Process(
        TagHelperContext context,
        TagHelperOutput output)
    {
        if (string.IsNullOrWhiteSpace(Name) ||
            !OrizonIconRegistry.TryGet(Name, out var iconContent))
        {
            output.SuppressOutput();
            return;
        }

        var isDecorative = string.IsNullOrWhiteSpace(Label);

        output.TagName = "svg";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("xmlns", "http://www.w3.org/2000/svg");
        output.Attributes.SetAttribute("width", Size);
        output.Attributes.SetAttribute("height", Size);
        output.Attributes.SetAttribute("viewBox", "0 0 24 24");
        output.Attributes.SetAttribute("fill", "none");
        output.Attributes.SetAttribute("stroke", "currentColor");
        output.Attributes.SetAttribute(
            "stroke-width",
            StrokeWidth.ToString(
                System.Globalization.CultureInfo.InvariantCulture));
        output.Attributes.SetAttribute("stroke-linecap", "round");
        output.Attributes.SetAttribute("stroke-linejoin", "round");
        output.Attributes.SetAttribute(
            "class",
            BuildCssClass(CssClass));

        if (isDecorative)
        {
            output.Attributes.SetAttribute("aria-hidden", "true");
            output.Attributes.SetAttribute("focusable", "false");
        }
        else
        {
            output.Attributes.SetAttribute("role", "img");
            output.Attributes.SetAttribute("aria-label", Label);
        }

        output.Content.SetHtmlContent(iconContent);
    }

    private static string BuildCssClass(string? cssClass)
    {
        return string.IsNullOrWhiteSpace(cssClass)
            ? "orizon-icon"
            : $"orizon-icon {cssClass}";
    }
}