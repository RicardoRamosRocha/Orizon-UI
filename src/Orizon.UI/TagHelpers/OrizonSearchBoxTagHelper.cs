using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orizon.UI.TagHelpers;

[HtmlTargetElement("orizon-search-box")]
public sealed class OrizonSearchBoxTagHelper : TagHelper
{
    [HtmlAttributeName("name")] public string Name { get; set; } = "search";
    [HtmlAttributeName("value")] public string? Value { get; set; }
    [HtmlAttributeName("placeholder")] public string Placeholder { get; set; } = "Pesquisar...";
    [HtmlAttributeName("label")] public string Label { get; set; } = "Pesquisar";
    [HtmlAttributeName("debounce")] public int Debounce { get; set; } = 300;
    [HtmlAttributeName("loading")] public bool Loading { get; set; }
    [HtmlAttributeName("full-width")] public bool FullWidth { get; set; }
    [HtmlAttributeName("disabled")] public bool Disabled { get; set; }
    [HtmlAttributeName("shortcut")] public bool Shortcut { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var e = HtmlEncoder.Default; var id = output.Attributes["id"]?.Value?.ToString() ?? $"orizon-search-{context.UniqueId.Replace("__", "")}"; var cls = output.Attributes["class"]?.Value?.ToString(); output.Attributes.RemoveAll("class"); output.Attributes.RemoveAll("id");
        output.TagName = "div"; output.TagMode = TagMode.StartTagAndEndTag; output.Attributes.SetAttribute("class", TagHelperClassBuilder.Build("orizon-search-box", FullWidth ? "orizon-search-box--full" : null, Loading ? "is-loading" : null, Disabled ? "is-disabled" : null, cls)); output.Attributes.SetAttribute("data-orizon-search", ""); output.Attributes.SetAttribute("data-debounce", Math.Clamp(Debounce, 0, 5000)); if (Shortcut) output.Attributes.SetAttribute("data-search-shortcut", "true");
        output.Content.SetHtmlContent($"<label class=\"orizon-visually-hidden\" for=\"{e.Encode(id)}\">{e.Encode(Label)}</label><span class=\"orizon-search-box__icon\" aria-hidden=\"true\">⌕</span><input class=\"orizon-search-box__input\" id=\"{e.Encode(id)}\" type=\"search\" name=\"{e.Encode(Name)}\" value=\"{e.Encode(Value ?? "")}\" placeholder=\"{e.Encode(Placeholder)}\" autocomplete=\"off\"{(Disabled ? " disabled" : "")}><span class=\"orizon-search-box__spinner\" aria-hidden=\"true\"></span><button class=\"orizon-search-box__clear\" type=\"button\" data-search-clear aria-label=\"Limpar pesquisa\"{(string.IsNullOrWhiteSpace(Value) ? " hidden" : "")}><span aria-hidden=\"true\">&times;</span></button>{(Shortcut ? "<kbd class=\"orizon-search-box__shortcut\">Ctrl K</kbd>" : "")}");
    }
}
