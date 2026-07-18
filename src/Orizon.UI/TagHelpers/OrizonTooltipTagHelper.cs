using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orizon.UI.TagHelpers;

[HtmlTargetElement("orizon-tooltip")]
public sealed class OrizonTooltipTagHelper : TagHelper
{
    [HtmlAttributeName("text")] public string Text { get; set; } = string.Empty;
    [HtmlAttributeName("position")] public string Position { get; set; } = "top";
    [HtmlAttributeName("delay")] public int Delay { get; set; } = 250;
    [HtmlAttributeName("disabled")] public bool Disabled { get; set; }
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var content = await output.GetChildContentAsync(); if (Disabled) { output.TagName = null; output.Content.SetHtmlContent(content); return; }
        var id = $"orizon-tooltip-{context.UniqueId.Replace("__", "")}"; var cls = output.Attributes["class"]?.Value?.ToString(); output.Attributes.RemoveAll("class");
        output.TagName = "span"; output.TagMode = TagMode.StartTagAndEndTag; output.Attributes.SetAttribute("class", TagHelperClassBuilder.Build("orizon-tooltip", $"orizon-tooltip--{Position}", cls)); output.Attributes.SetAttribute("data-orizon-tooltip", ""); output.Attributes.SetAttribute("style", $"--orizon-tooltip-delay:{Math.Max(0, Delay)}ms");
        output.Content.SetHtmlContent($"<span class=\"orizon-tooltip__anchor\" aria-describedby=\"{id}\">{content.GetContent()}</span><span class=\"orizon-tooltip__bubble\" id=\"{id}\" role=\"tooltip\">{HtmlEncoder.Default.Encode(Text)}</span>");
    }
}
