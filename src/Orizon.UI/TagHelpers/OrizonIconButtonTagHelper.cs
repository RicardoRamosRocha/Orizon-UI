using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orizon.UI.TagHelpers;

[HtmlTargetElement("orizon-icon-button")]
public sealed class OrizonIconButtonTagHelper : TagHelper
{
    [HtmlAttributeName("icon")] public string Icon { get; set; } = string.Empty;
    [HtmlAttributeName("label")] public string Label { get; set; } = string.Empty;
    [HtmlAttributeName("tooltip")] public string? Tooltip { get; set; }
    [HtmlAttributeName("variant")] public string Variant { get; set; } = "soft";
    [HtmlAttributeName("color")] public string Color { get; set; } = "primary";
    [HtmlAttributeName("size")] public string Size { get; set; } = "md";
    [HtmlAttributeName("shape")] public string Shape { get; set; } = "square";
    [HtmlAttributeName("loading")] public bool Loading { get; set; }
    [HtmlAttributeName("disabled")] public bool Disabled { get; set; }
    [HtmlAttributeName("type")] public string Type { get; set; } = "button";
    [HtmlAttributeName("href")] public string? Href { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var cls = output.Attributes["class"]?.Value?.ToString(); output.Attributes.RemoveAll("class");
        output.TagName = string.IsNullOrWhiteSpace(Href) ? "button" : "a"; output.TagMode = TagMode.StartTagAndEndTag;
        output.Attributes.SetAttribute("class", TagHelperClassBuilder.Build("orizon-icon-button", $"orizon-button--{Variant}", $"orizon-button--{Color}", $"orizon-icon-button--{Size}", $"orizon-icon-button--{Shape}", Loading ? "is-loading" : null, cls));
        output.Attributes.SetAttribute("aria-label", Label); if (!string.IsNullOrWhiteSpace(Tooltip)) output.Attributes.SetAttribute("title", Tooltip);
        if (output.TagName == "a") output.Attributes.SetAttribute("href", Href); else output.Attributes.SetAttribute("type", Type);
        if (Disabled || Loading) { if (output.TagName == "button") output.Attributes.SetAttribute("disabled", "disabled"); else { output.Attributes.SetAttribute("aria-disabled", "true"); output.Attributes.SetAttribute("tabindex", "-1"); } }
        if (Loading) output.Attributes.SetAttribute("aria-busy", "true");
        output.Content.SetHtmlContent(Loading ? "<span class=\"orizon-button__spinner\" aria-hidden=\"true\"></span>" : $"<i class=\"ph ph-{HtmlEncoder.Default.Encode(Icon)}\" aria-hidden=\"true\"></i>");
    }
}
