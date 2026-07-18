using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orizon.UI.TagHelpers;

[HtmlTargetElement("orizon-stat-card")]
public sealed class OrizonStatCardTagHelper : TagHelper
{
    [HtmlAttributeName("title")] public string Title { get; set; } = string.Empty;
    [HtmlAttributeName("value")] public string Value { get; set; } = string.Empty;
    [HtmlAttributeName("icon")] public string? Icon { get; set; }
    [HtmlAttributeName("description")] public string? Description { get; set; }
    [HtmlAttributeName("trend")] public string? Trend { get; set; }
    [HtmlAttributeName("trend-type")] public string TrendType { get; set; } = "neutral";
    [HtmlAttributeName("color")] public string Color { get; set; } = "primary";
    [HtmlAttributeName("href")] public string? Href { get; set; }
    [HtmlAttributeName("action-label")] public string? ActionLabel { get; set; }
    [HtmlAttributeName("loading")] public bool Loading { get; set; }
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var e = HtmlEncoder.Default; var actionable = !string.IsNullOrWhiteSpace(Href); var cls = output.Attributes["class"]?.Value?.ToString(); output.Attributes.RemoveAll("class"); output.TagName = actionable ? "a" : "article"; output.TagMode = TagMode.StartTagAndEndTag;
        output.Attributes.SetAttribute("class", TagHelperClassBuilder.Build("orizon-stat-card", $"orizon-stat-card--{Color}", actionable ? "orizon-stat-card--actionable" : null, Loading ? "is-loading" : null, cls)); if (actionable) { output.Attributes.SetAttribute("href", Href); if (!string.IsNullOrWhiteSpace(ActionLabel)) output.Attributes.SetAttribute("aria-label", ActionLabel); }
        if (Loading) { output.Attributes.SetAttribute("aria-busy", "true"); output.Content.SetHtmlContent("<span class=\"orizon-skeleton orizon-skeleton--circle is-animated\" aria-hidden=\"true\"></span><span class=\"orizon-stat-card__body\"><span class=\"orizon-skeleton orizon-skeleton--text is-animated\" aria-hidden=\"true\"></span><span class=\"orizon-skeleton orizon-skeleton--rectangle is-animated\" aria-hidden=\"true\"></span></span>"); return; }
        var icon = string.IsNullOrWhiteSpace(Icon) ? "" : $"<span class=\"orizon-stat-card__icon\"><i class=\"ph ph-{e.Encode(Icon)}\" aria-hidden=\"true\"></i></span>"; var trendLabel = TrendType switch { "positive" => "Aumento", "negative" => "Redução", _ => "Sem alteração" }; var trend = string.IsNullOrWhiteSpace(Trend) ? "" : $"<span class=\"orizon-stat-card__trend orizon-stat-card__trend--{e.Encode(TrendType)}\"><span aria-hidden=\"true\">{(TrendType == "positive" ? "↑" : TrendType == "negative" ? "↓" : "→")}</span><span class=\"orizon-visually-hidden\">{trendLabel}: </span>{e.Encode(Trend)}</span>";
        output.Content.SetHtmlContent($"{icon}<span class=\"orizon-stat-card__body\"><span class=\"orizon-stat-card__title\">{e.Encode(Title)}</span><strong class=\"orizon-stat-card__value\">{e.Encode(Value)}</strong><span class=\"orizon-stat-card__meta\">{trend}{(Description is null ? "" : $"<span class=\"orizon-stat-card__description\">{e.Encode(Description)}</span>")}</span></span>");
    }
}
