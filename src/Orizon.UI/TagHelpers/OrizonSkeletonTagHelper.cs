using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orizon.UI.TagHelpers;

[HtmlTargetElement("orizon-skeleton")]
public sealed class OrizonSkeletonTagHelper : TagHelper
{
    [HtmlAttributeName("variant")] public string Variant { get; set; } = "text";
    [HtmlAttributeName("width")] public string? Width { get; set; }
    [HtmlAttributeName("height")] public string? Height { get; set; }
    [HtmlAttributeName("lines")] public int Lines { get; set; } = 1;
    [HtmlAttributeName("radius")] public string? Radius { get; set; }
    [HtmlAttributeName("animated")] public bool Animated { get; set; } = true;
    [HtmlAttributeName("aria-label")] public string? AriaLabel { get; set; }
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "span"; output.TagMode = TagMode.StartTagAndEndTag; var cls = output.Attributes["class"]?.Value?.ToString(); output.Attributes.RemoveAll("class");
        output.Attributes.SetAttribute("class", TagHelperClassBuilder.Build("orizon-skeleton", $"orizon-skeleton--{Variant}", Animated ? "is-animated" : null, cls));
        var styles = new[] { Width is null ? null : $"width:{Width}", Height is null ? null : $"height:{Height}", Radius is null ? null : $"border-radius:{Radius}" }; output.Attributes.SetAttribute("style", string.Join(';', styles.Where(x => x is not null)));
        if (string.IsNullOrWhiteSpace(AriaLabel)) output.Attributes.SetAttribute("aria-hidden", "true"); else { output.Attributes.SetAttribute("role", "status"); output.Attributes.SetAttribute("aria-label", AriaLabel); }
        if (Variant.Equals("text", StringComparison.OrdinalIgnoreCase) && Lines > 1) output.Content.SetHtmlContent(string.Concat(Enumerable.Range(0, Math.Clamp(Lines, 1, 20)).Select(i => $"<span class=\"orizon-skeleton__line\"{(i == Lines - 1 ? " style=\"width:72%\"" : "")}></span>")));
    }
}
