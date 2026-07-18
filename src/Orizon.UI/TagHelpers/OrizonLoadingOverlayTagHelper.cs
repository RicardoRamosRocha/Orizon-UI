using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orizon.UI.TagHelpers;

[HtmlTargetElement("orizon-loading-overlay")]
public sealed class OrizonLoadingOverlayTagHelper : TagHelper
{
    [HtmlAttributeName("active")] public bool Active { get; set; } = true;
    [HtmlAttributeName("text")] public string Text { get; set; } = "Carregando...";
    [HtmlAttributeName("blur")] public bool Blur { get; set; } = true;
    [HtmlAttributeName("opacity")] public decimal Opacity { get; set; } = .72m;
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var content = await output.GetChildContentAsync(); var cls = output.Attributes["class"]?.Value?.ToString(); output.Attributes.RemoveAll("class"); output.TagName = "div"; output.TagMode = TagMode.StartTagAndEndTag; output.Attributes.SetAttribute("class", TagHelperClassBuilder.Build("orizon-loading-region", Active ? "is-loading" : null, Blur ? "has-blur" : null, cls)); output.Attributes.SetAttribute("data-orizon-loading-overlay", ""); output.Attributes.SetAttribute("aria-busy", Active.ToString().ToLowerInvariant());
        output.Content.SetHtmlContent($"<div class=\"orizon-loading-region__content\"{(Active ? " aria-hidden=\"true\"" : "")}>{content.GetContent()}</div><div class=\"orizon-loading-overlay\" role=\"status\" style=\"--orizon-overlay-opacity:{Math.Clamp(Opacity, 0, 1).ToString(System.Globalization.CultureInfo.InvariantCulture)}\"{(Active ? "" : " hidden")}><span class=\"orizon-loading-overlay__spinner\" aria-hidden=\"true\"></span><span>{HtmlEncoder.Default.Encode(Text)}</span></div>");
    }
}
