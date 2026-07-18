using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orizon.UI.TagHelpers;

[HtmlTargetElement("orizon-kpi-card")]
public sealed class OrizonKpiCardTagHelper : TagHelper
{
    [HtmlAttributeName("title")] public string Title { get; set; } = string.Empty;
    [HtmlAttributeName("subtitle")] public string? Subtitle { get; set; }
    [HtmlAttributeName("value")] public string Value { get; set; } = string.Empty;
    [HtmlAttributeName("indicator")] public string? Indicator { get; set; }
    [HtmlAttributeName("icon")] public string? Icon { get; set; }
    [HtmlAttributeName("percentage")] public string? Percentage { get; set; }
    [HtmlAttributeName("trend-type")] public string TrendType { get; set; } = "neutral";
    [HtmlAttributeName("color")] public string Color { get; set; } = "primary";
    [HtmlAttributeName("loading")] public bool Loading { get; set; }
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var e = HtmlEncoder.Default; var cls = output.Attributes["class"]?.Value?.ToString(); output.Attributes.RemoveAll("class"); output.TagName = "article"; output.TagMode = TagMode.StartTagAndEndTag; output.Attributes.SetAttribute("class", TagHelperClassBuilder.Build("orizon-kpi-card", $"orizon-kpi-card--{Color}", Loading ? "is-loading" : null, cls)); if (Loading) { output.Attributes.SetAttribute("aria-busy", "true"); output.Content.SetHtmlContent("<span class=\"orizon-skeleton orizon-skeleton--text is-animated\" aria-hidden=\"true\"></span><span class=\"orizon-skeleton orizon-skeleton--rectangle is-animated\" aria-hidden=\"true\"></span>"); return; }
        var icon = Icon is null ? "" : $"<span class=\"orizon-kpi-card__icon\" aria-hidden=\"true\"><i class=\"ph ph-{e.Encode(Icon)}\"></i></span>"; var percent = Percentage is null ? "" : $"<span class=\"orizon-kpi-card__percentage orizon-kpi-card__percentage--{e.Encode(TrendType)}\"><span aria-hidden=\"true\">{(TrendType == "positive" ? "↑" : TrendType == "negative" ? "↓" : "→")}</span>{e.Encode(Percentage)}</span>";
        output.Content.SetHtmlContent($"<header>{icon}<div><span class=\"orizon-kpi-card__title\">{e.Encode(Title)}</span>{(Subtitle is null ? "" : $"<span class=\"orizon-kpi-card__subtitle\">{e.Encode(Subtitle)}</span>")}</div></header><div class=\"orizon-kpi-card__metric\"><strong>{e.Encode(Value)}</strong>{percent}</div>{(Indicator is null ? "" : $"<div class=\"orizon-kpi-card__indicator\"><span style=\"width:{e.Encode(Indicator)}\"></span></div>")}");
    }
}
