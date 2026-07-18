using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orizon.UI.TagHelpers;

[HtmlTargetElement("orizon-data-table")]
public sealed class OrizonDataTableTagHelper : TagHelper
{
    [HtmlAttributeName("label")] public string Label { get; set; } = "Tabela de dados";
    [HtmlAttributeName("striped")] public bool Striped { get; set; }
    [HtmlAttributeName("hover")] public bool Hover { get; set; }
    [HtmlAttributeName("pageable")] public bool Pageable { get; set; }
    [HtmlAttributeName("sortable")] public bool Sortable { get; set; }
    [HtmlAttributeName("searchable")] public bool Searchable { get; set; }
    [HtmlAttributeName("selectable")] public bool Selectable { get; set; }
    [HtmlAttributeName("compact")] public bool Compact { get; set; }
    [HtmlAttributeName("responsive")] public bool Responsive { get; set; } = true;
    [HtmlAttributeName("sticky-header")] public bool StickyHeader { get; set; }
    [HtmlAttributeName("density")] public string Density { get; set; } = "default";
    [HtmlAttributeName("loading")] public bool Loading { get; set; }
    [HtmlAttributeName("empty")] public bool Empty { get; set; }
    [HtmlAttributeName("empty-title")] public string EmptyTitle { get; set; } = "Nenhum resultado encontrado";
    [HtmlAttributeName("page")] public int Page { get; set; } = 1;
    [HtmlAttributeName("total-pages")] public int TotalPages { get; set; } = 1;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var e = HtmlEncoder.Default; var content = await output.GetChildContentAsync(); var cls = output.Attributes["class"]?.Value?.ToString(); output.Attributes.RemoveAll("class"); output.TagName = "section"; output.TagMode = TagMode.StartTagAndEndTag; output.Attributes.SetAttribute("class", TagHelperClassBuilder.Build("orizon-data-table", $"orizon-data-table--density-{Density}", StickyHeader ? "orizon-data-table--sticky" : null, Responsive ? "orizon-data-table--responsive" : null, Loading ? "is-loading" : null, cls)); output.Attributes.SetAttribute("data-orizon-data-table", ""); output.Attributes.SetAttribute("aria-label", Label); if (Loading) output.Attributes.SetAttribute("aria-busy", "true");
        var tools = Searchable ? "<div class=\"orizon-data-table__tools\"><div class=\"orizon-search-box orizon-search-box--full\" data-orizon-search data-debounce=\"300\"><label class=\"orizon-visually-hidden\" for=\"orizon-data-table-search\">Pesquisar na tabela</label><span class=\"orizon-search-box__icon\" aria-hidden=\"true\">⌕</span><input class=\"orizon-search-box__input\" id=\"orizon-data-table-search\" type=\"search\" placeholder=\"Pesquisar na tabela...\" autocomplete=\"off\"><button class=\"orizon-search-box__clear\" type=\"button\" data-search-clear aria-label=\"Limpar pesquisa\" hidden>&times;</button></div></div>" : "";
        var body = Loading ? string.Concat(Enumerable.Repeat("<span class=\"orizon-skeleton orizon-skeleton--table-row is-animated\" aria-hidden=\"true\"></span>", 5)) : Empty ? $"<div class=\"orizon-data-table__empty\"><strong>{e.Encode(EmptyTitle)}</strong><span>Tente ajustar a pesquisa ou os filtros.</span></div>" : $"<div class=\"orizon-data-table__scroll\" role=\"region\" tabindex=\"0\" aria-label=\"{e.Encode(Label)}\"><table class=\"orizon-data-table__table{(Striped ? " is-striped" : "")}{(Hover ? " is-hoverable" : "")}{(Compact ? " is-compact" : "")}\" data-sortable=\"{Sortable.ToString().ToLowerInvariant()}\" data-selectable=\"{Selectable.ToString().ToLowerInvariant()}\">{content.GetContent()}</table></div>";
        var footer = Pageable ? $"<footer class=\"orizon-data-table__footer\"><span>Página {Math.Clamp(Page, 1, Math.Max(1, TotalPages))} de {Math.Max(1, TotalPages)}</span><div><button type=\"button\" data-table-page=\"prev\"{(Page <= 1 ? " disabled" : "")}>Anterior</button><button type=\"button\" data-table-page=\"next\"{(Page >= TotalPages ? " disabled" : "")}>Próxima</button></div></footer>" : "";
        output.Content.SetHtmlContent($"{tools}<div class=\"orizon-data-table__body\">{body}</div>{footer}");
    }
}
