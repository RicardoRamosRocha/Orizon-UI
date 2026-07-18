using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orizon.UI.TagHelpers;

[HtmlTargetElement("orizon-filter-bar")]
public sealed class OrizonFilterBarTagHelper : TagHelper
{
    [HtmlAttributeName("label")] public string Label { get; set; } = "Filtros ativos";
    [HtmlAttributeName("count")] public int? Count { get; set; }
    [HtmlAttributeName("clear-label")] public string ClearLabel { get; set; } = "Limpar todos";
    [HtmlAttributeName("show-clear")] public bool ShowClear { get; set; } = true;
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var e = HtmlEncoder.Default; var content = await output.GetChildContentAsync(); var cls = output.Attributes["class"]?.Value?.ToString(); output.Attributes.RemoveAll("class"); output.TagName = "div"; output.TagMode = TagMode.StartTagAndEndTag; output.Attributes.SetAttribute("class", TagHelperClassBuilder.Build("orizon-filter-bar", cls)); output.Attributes.SetAttribute("data-orizon-filter-bar", ""); output.Attributes.SetAttribute("aria-label", Label);
        var counter = Count.HasValue ? $"<span class=\"orizon-filter-bar__count\" aria-label=\"{Count} filtros ativos\">{Count}</span>" : ""; var clear = ShowClear ? $"<button class=\"orizon-filter-bar__clear\" type=\"button\" data-filter-clear>{e.Encode(ClearLabel)}</button>" : ""; output.Content.SetHtmlContent($"<div class=\"orizon-filter-bar__content\">{content.GetContent()}</div>{counter}{clear}");
    }
}
