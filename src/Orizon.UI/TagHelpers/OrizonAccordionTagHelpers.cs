using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orizon.UI.TagHelpers;

[HtmlTargetElement("orizon-accordion")]
[RestrictChildren("orizon-accordion-item")]
public sealed class OrizonAccordionTagHelper : TagHelper
{
    [HtmlAttributeName("mode")] public string Mode { get; set; } = "single";
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var id = output.Attributes["id"]?.Value?.ToString() ?? $"orizon-accordion-{context.UniqueId.Replace("__", "")}";
        context.Items[typeof(OrizonAccordionTagHelper)] = id;
        output.TagName = "div"; output.TagMode = TagMode.StartTagAndEndTag; output.Attributes.SetAttribute("id", id); output.Attributes.SetAttribute("data-orizon-accordion", Mode.Equals("multiple", StringComparison.OrdinalIgnoreCase) ? "multiple" : "single");
        var cls = output.Attributes["class"]?.Value?.ToString(); output.Attributes.SetAttribute("class", TagHelperClassBuilder.Build("orizon-accordion", cls)); output.Content.SetHtmlContent(await output.GetChildContentAsync());
    }
}

[HtmlTargetElement("orizon-accordion-item", ParentTag = "orizon-accordion")]
public sealed class OrizonAccordionItemTagHelper : TagHelper
{
    [HtmlAttributeName("title")] public string Title { get; set; } = string.Empty;
    [HtmlAttributeName("icon")] public string? Icon { get; set; }
    [HtmlAttributeName("expanded")] public bool Expanded { get; set; }
    [HtmlAttributeName("disabled")] public bool Disabled { get; set; }
    [HtmlAttributeName("identifier")] public string? Identifier { get; set; }
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var e = HtmlEncoder.Default; var parent = context.Items.TryGetValue(typeof(OrizonAccordionTagHelper), out var p) ? p?.ToString() : "orizon-accordion"; var key = Identifier ?? context.UniqueId.Replace("__", ""); var trigger = $"{parent}-{key}-trigger"; var panel = $"{parent}-{key}-panel"; var content = await output.GetChildContentAsync();
        output.TagName = "section"; output.TagMode = TagMode.StartTagAndEndTag; output.Attributes.SetAttribute("class", TagHelperClassBuilder.Build("orizon-accordion__item", Expanded ? "is-expanded" : null, Disabled ? "is-disabled" : null)); output.Attributes.SetAttribute("data-accordion-item", "");
        var icon = string.IsNullOrWhiteSpace(Icon) ? "" : $"<i class=\"ph ph-{e.Encode(Icon)}\" aria-hidden=\"true\"></i>";
        output.Content.SetHtmlContent($"<h3 class=\"orizon-accordion__heading\"><button class=\"orizon-accordion__trigger\" type=\"button\" id=\"{e.Encode(trigger)}\" aria-expanded=\"{Expanded.ToString().ToLowerInvariant()}\" aria-controls=\"{e.Encode(panel)}\"{(Disabled ? " disabled" : "")}>{icon}<span>{e.Encode(Title)}</span><span class=\"orizon-accordion__chevron\" aria-hidden=\"true\">⌄</span></button></h3><div class=\"orizon-accordion__panel\" id=\"{e.Encode(panel)}\" role=\"region\" aria-labelledby=\"{e.Encode(trigger)}\"{(Expanded ? "" : " hidden")}><div class=\"orizon-accordion__content\">{content.GetContent()}</div></div>");
    }
}
