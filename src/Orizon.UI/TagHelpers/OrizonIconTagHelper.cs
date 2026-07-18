using Microsoft.AspNetCore.Razor.TagHelpers;
using Orizon.UI.Icons;

namespace Orizon.UI.TagHelpers;

[HtmlTargetElement("orizon-icon")]
public sealed class OrizonIconTagHelper : TagHelper
{
    [HtmlAttributeName("name")] public string Name { get; set; } = string.Empty;
    [HtmlAttributeName("size")] public int Size { get; set; } = 20;
    [HtmlAttributeName("weight")] public string Weight { get; set; } = "regular";
    [HtmlAttributeName("stroke-width")] public decimal? StrokeWidth { get; set; }
    [HtmlAttributeName("color")] public string? Color { get; set; }
    [HtmlAttributeName("label")] public string? Label { get; set; }
    [HtmlAttributeName("aria-label")] public string? AriaLabel { get; set; }
    [HtmlAttributeName("aria-hidden")] public bool? AriaHidden { get; set; }
    [HtmlAttributeName("class")] public string? CssClass { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (string.IsNullOrWhiteSpace(Name) || !OrizonIconRegistry.TryGet(Name, out var content)) { output.SuppressOutput(); return; }
        var label = AriaLabel ?? Label;
        var decorative = AriaHidden ?? string.IsNullOrWhiteSpace(label);
        var stroke = StrokeWidth ?? (Weight.ToLowerInvariant() switch { "thin" => 1m, "light" => 1.4m, "bold" => 2.5m, "fill" => 2.2m, _ => 1.8m });
        output.TagName = "svg"; output.TagMode = TagMode.StartTagAndEndTag;
        output.Attributes.SetAttribute("xmlns", "http://www.w3.org/2000/svg");
        output.Attributes.SetAttribute("width", Size); output.Attributes.SetAttribute("height", Size);
        output.Attributes.SetAttribute("viewBox", "0 0 24 24"); output.Attributes.SetAttribute("fill", Weight.Equals("fill", StringComparison.OrdinalIgnoreCase) ? "currentColor" : "none");
        output.Attributes.SetAttribute("stroke", "currentColor"); output.Attributes.SetAttribute("stroke-width", stroke.ToString(System.Globalization.CultureInfo.InvariantCulture));
        output.Attributes.SetAttribute("stroke-linecap", "round"); output.Attributes.SetAttribute("stroke-linejoin", "round");
        output.Attributes.SetAttribute("class", TagHelperClassBuilder.Build("orizon-icon", $"orizon-icon--{Weight.ToLowerInvariant()}", CssClass));
        if (!string.IsNullOrWhiteSpace(Color)) output.Attributes.SetAttribute("style", $"color:var(--orizon-color-{Color}, {Color})");
        if (decorative) { output.Attributes.SetAttribute("aria-hidden", "true"); output.Attributes.SetAttribute("focusable", "false"); }
        else { output.Attributes.SetAttribute("role", "img"); output.Attributes.SetAttribute("aria-label", label); }
        output.Content.SetHtmlContent(content);
    }
}
