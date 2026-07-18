using System.Collections;
using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orizon.UI.TagHelpers;

[HtmlTargetElement("orizon-enterprise-grid")]
public sealed class OrizonEnterpriseGridTagHelper : TagHelper
{
    [HtmlAttributeName("items")] public IEnumerable? Items { get; set; }
    [HtmlAttributeName("key-field")] public string KeyField { get; set; } = "Id";
    [HtmlAttributeName("label")] public string Label { get; set; } = "Grade de dados";
    [HtmlAttributeName("height")] public string Height { get; set; } = "32rem";
    [HtmlAttributeName("row-height")] public int RowHeight { get; set; } = 44;
    [HtmlAttributeName("virtual-scroll")] public bool VirtualScroll { get; set; } = true;
    [HtmlAttributeName("virtual-columns")] public bool VirtualColumns { get; set; } = true;
    [HtmlAttributeName("selectable")] public string Selectable { get; set; } = "multiple";
    [HtmlAttributeName("cell-selection")] public bool CellSelection { get; set; } = true;
    [HtmlAttributeName("sortable")] public bool Sortable { get; set; } = true;
    [HtmlAttributeName("filterable")] public bool Filterable { get; set; } = true;
    [HtmlAttributeName("groupable")] public bool Groupable { get; set; } = true;
    [HtmlAttributeName("persist-key")] public string? PersistKey { get; set; }
    [HtmlAttributeName("infinite-scroll")] public bool InfiniteScroll { get; set; }
    [HtmlAttributeName("server-mode")] public bool ServerMode { get; set; }
    [HtmlAttributeName("server-url")] public string? ServerUrl { get; set; }
    [HtmlAttributeName("edit-mode")] public string EditMode { get; set; } = "readonly";

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var content = await output.GetChildContentAsync();
        var options = new JsonSerializerOptions { Encoder = JavaScriptEncoder.Default };
        var data = JsonSerializer.Serialize(Items ?? Array.Empty<object>(), options).Replace("</", "<\\/");
        var id = output.Attributes["id"]?.Value?.ToString() ?? $"orizon-grid-{context.UniqueId.Replace("__", "")}";
        var existingClass = output.Attributes["class"]?.Value?.ToString();
        output.Attributes.Clear();
        output.TagName = "section";
        output.TagMode = TagMode.StartTagAndEndTag;
        output.Attributes.SetAttribute("id", id);
        output.Attributes.SetAttribute("class", TagHelperClassBuilder.Build("orizon-enterprise-grid", existingClass));
        output.Attributes.SetAttribute("data-orizon-enterprise-grid", "");
        output.Attributes.SetAttribute("data-key-field", KeyField);
        output.Attributes.SetAttribute("data-row-height", Math.Clamp(RowHeight, 28, 96));
        output.Attributes.SetAttribute("data-virtual-scroll", VirtualScroll.ToString().ToLowerInvariant());
        output.Attributes.SetAttribute("data-virtual-columns", VirtualColumns.ToString().ToLowerInvariant());
        output.Attributes.SetAttribute("data-selectable", Selectable);
        output.Attributes.SetAttribute("data-cell-selection", CellSelection.ToString().ToLowerInvariant());
        output.Attributes.SetAttribute("data-sortable", Sortable.ToString().ToLowerInvariant());
        output.Attributes.SetAttribute("data-filterable", Filterable.ToString().ToLowerInvariant());
        output.Attributes.SetAttribute("data-groupable", Groupable.ToString().ToLowerInvariant());
        output.Attributes.SetAttribute("data-infinite-scroll", InfiniteScroll.ToString().ToLowerInvariant());
        output.Attributes.SetAttribute("data-server-mode", ServerMode.ToString().ToLowerInvariant());
        output.Attributes.SetAttribute("data-edit-mode", EditMode);
        if (PersistKey is not null) output.Attributes.SetAttribute("data-persist-key", PersistKey);
        if (ServerUrl is not null) output.Attributes.SetAttribute("data-server-url", ServerUrl);
        output.Attributes.SetAttribute("aria-label", Label);
        output.Attributes.SetAttribute("style", $"--orizon-grid-height:{Height};--orizon-grid-row-height:{Math.Clamp(RowHeight, 28, 96)}px");
        output.Content.SetHtmlContent($$"""
            <script type="application/json" data-grid-data>{{data}}</script>
            <div class="orizon-enterprise-grid__extensions">{{content.GetContent()}}</div>
            <div class="orizon-enterprise-grid__status" role="status" aria-live="polite" data-grid-status></div>
            <div class="orizon-enterprise-grid__viewport" role="grid" tabindex="0" aria-label="{{HtmlEncoder.Default.Encode(Label)}}" aria-rowcount="0" aria-colcount="0" data-grid-viewport>
              <div class="orizon-enterprise-grid__header" role="rowgroup"><div class="orizon-enterprise-grid__header-row" role="row" data-grid-header></div></div>
              <div class="orizon-enterprise-grid__body" role="rowgroup" data-grid-body><div class="orizon-enterprise-grid__spacer" data-grid-spacer></div><div class="orizon-enterprise-grid__rows" data-grid-rows></div></div>
              <div class="orizon-enterprise-grid__footer" data-grid-footer></div>
            </div>
            <textarea class="orizon-visually-hidden" aria-hidden="true" tabindex="-1" data-grid-clipboard></textarea>
            """);
    }
}

[HtmlTargetElement("orizon-grid-column", ParentTag = "orizon-enterprise-grid")]
public sealed class OrizonGridColumnTagHelper : TagHelper
{
    [HtmlAttributeName("field")] public string Field { get; set; } = string.Empty;
    [HtmlAttributeName("title")] public string? Title { get; set; }
    [HtmlAttributeName("width")] public int? Width { get; set; }
    [HtmlAttributeName("min-width")] public int MinWidth { get; set; } = 80;
    [HtmlAttributeName("flex")] public int? Flex { get; set; }
    [HtmlAttributeName("format")] public string? Format { get; set; }
    [HtmlAttributeName("type")] public string Type { get; set; } = "text";
    [HtmlAttributeName("sortable")] public bool Sortable { get; set; } = true;
    [HtmlAttributeName("filterable")] public bool Filterable { get; set; } = true;
    [HtmlAttributeName("resizable")] public bool Resizable { get; set; } = true;
    [HtmlAttributeName("pinned")] public string? Pinned { get; set; }
    [HtmlAttributeName("hidden")] public bool Hidden { get; set; }
    [HtmlAttributeName("editable")] public bool Editable { get; set; }
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "script"; output.TagMode = TagMode.StartTagAndEndTag; output.Attributes.Clear();
        output.Attributes.SetAttribute("type", "application/json"); output.Attributes.SetAttribute("data-grid-column", "");
        output.Content.SetHtmlContent(JsonSerializer.Serialize(new { field = Field, title = Title ?? Field, width = Width, minWidth = MinWidth, flex = Flex, format = Format, type = Type, sortable = Sortable, filterable = Filterable, resizable = Resizable, pinned = Pinned, hidden = Hidden, editable = Editable }));
    }
}

[HtmlTargetElement("orizon-grid-toolbar", ParentTag = "orizon-enterprise-grid")]
public sealed class OrizonGridToolbarTagHelper : TagHelper
{
    [HtmlAttributeName("search")] public bool Search { get; set; } = true;
    [HtmlAttributeName("add")] public bool Add { get; set; }
    [HtmlAttributeName("edit")] public bool Edit { get; set; }
    [HtmlAttributeName("delete")] public bool Delete { get; set; }
    [HtmlAttributeName("export")] public bool Export { get; set; } = true;
    [HtmlAttributeName("refresh")] public bool Refresh { get; set; } = true;
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var content = await output.GetChildContentAsync(); output.TagName = "div"; output.TagMode = TagMode.StartTagAndEndTag; output.Attributes.Clear(); output.Attributes.SetAttribute("class", "orizon-grid-toolbar"); output.Attributes.SetAttribute("role", "toolbar"); output.Attributes.SetAttribute("aria-label", "Ações da grade"); output.Attributes.SetAttribute("data-grid-toolbar", "");
        output.Content.SetHtmlContent($"{(Search ? "<label class=\"orizon-grid-toolbar__search\"><span class=\"orizon-visually-hidden\">Pesquisar dados</span><input type=\"search\" placeholder=\"Pesquisar na grade...\" data-grid-quick-filter><kbd>Ctrl K</kbd></label>" : "")}<div class=\"orizon-grid-toolbar__actions\">{(Add ? "<button type=\"button\" data-grid-action=\"add\">Adicionar</button>" : "")}{(Edit ? "<button type=\"button\" data-grid-action=\"edit\">Editar</button>" : "")}{(Delete ? "<button type=\"button\" data-grid-action=\"delete\">Excluir</button>" : "")}{(Export ? "<button type=\"button\" data-grid-export>Exportar CSV</button>" : "")}{(Refresh ? "<button type=\"button\" data-grid-refresh>Atualizar</button>" : "")}{content.GetContent()}</div><span class=\"orizon-grid-toolbar__selection\" data-grid-selection-count>0 selecionados</span>");
    }
}

[HtmlTargetElement("orizon-grid-filter-panel", ParentTag = "orizon-enterprise-grid")]
public sealed class OrizonGridFilterPanelTagHelper : TagHelper
{
    [HtmlAttributeName("title")] public string Title { get; set; } = "Filtros avançados";
    [HtmlAttributeName("open")] public bool Open { get; set; }
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output) { var content = await output.GetChildContentAsync(); output.TagName = "aside"; output.TagMode = TagMode.StartTagAndEndTag; output.Attributes.SetAttribute("class", "orizon-grid-filter-panel"); output.Attributes.SetAttribute("data-grid-filter-panel", ""); if (!Open) output.Attributes.SetAttribute("hidden", "hidden"); output.Content.SetHtmlContent($"<header><h3>{HtmlEncoder.Default.Encode(Title)}</h3><button type=\"button\" data-grid-filter-close aria-label=\"Fechar filtros\">&times;</button></header><div>{content.GetContent()}<div data-grid-generated-filters></div></div>"); }
}

[HtmlTargetElement("orizon-grid-column-chooser", ParentTag = "orizon-enterprise-grid")]
public sealed class OrizonGridColumnChooserTagHelper : TagHelper
{
    [HtmlAttributeName("label")] public string Label { get; set; } = "Colunas";
    public override void Process(TagHelperContext context, TagHelperOutput output) { output.TagName = "div"; output.TagMode = TagMode.StartTagAndEndTag; output.Attributes.SetAttribute("class", "orizon-grid-column-chooser"); output.Attributes.SetAttribute("data-grid-column-chooser", ""); output.Content.SetHtmlContent($"<button type=\"button\" data-grid-column-chooser-toggle>{HtmlEncoder.Default.Encode(Label)}</button><div class=\"orizon-grid-column-chooser__menu\" data-grid-column-menu hidden></div>"); }
}

[HtmlTargetElement("orizon-grid-group-panel", ParentTag = "orizon-enterprise-grid")]
public sealed class OrizonGridGroupPanelTagHelper : TagHelper
{
    [HtmlAttributeName("placeholder")] public string Placeholder { get; set; } = "Arraste uma coluna aqui para agrupar";
    public override void Process(TagHelperContext context, TagHelperOutput output) { output.TagName = "div"; output.TagMode = TagMode.StartTagAndEndTag; output.Attributes.SetAttribute("class", "orizon-grid-group-panel"); output.Attributes.SetAttribute("data-grid-group-panel", ""); output.Attributes.SetAttribute("tabindex", "0"); output.Content.SetHtmlContent($"<span>{HtmlEncoder.Default.Encode(Placeholder)}</span><div data-grid-groups></div>"); }
}

[HtmlTargetElement("orizon-grid-summary-row", ParentTag = "orizon-enterprise-grid")]
public sealed class OrizonGridSummaryRowTagHelper : TagHelper
{
    [HtmlAttributeName("field")] public string? Field { get; set; }
    [HtmlAttributeName("type")] public string Type { get; set; } = "count";
    [HtmlAttributeName("label")] public string? Label { get; set; }
    public override void Process(TagHelperContext context, TagHelperOutput output) { output.TagName = "span"; output.TagMode = TagMode.StartTagAndEndTag; output.Attributes.SetAttribute("data-grid-summary", ""); if (Field is not null) output.Attributes.SetAttribute("data-field", Field); output.Attributes.SetAttribute("data-type", Type); if (Label is not null) output.Attributes.SetAttribute("data-label", Label); output.Attributes.SetAttribute("hidden", "hidden"); }
}

[HtmlTargetElement("orizon-grid-pager", ParentTag = "orizon-enterprise-grid")]
public sealed class OrizonGridPagerTagHelper : TagHelper
{
    [HtmlAttributeName("page-size")] public int PageSize { get; set; } = 50;
    [HtmlAttributeName("page-sizes")] public string PageSizes { get; set; } = "10,25,50,100,250,1000";
    public override void Process(TagHelperContext context, TagHelperOutput output) { output.TagName = "nav"; output.TagMode = TagMode.StartTagAndEndTag; output.Attributes.SetAttribute("class", "orizon-grid-pager"); output.Attributes.SetAttribute("aria-label", "Paginação da grade"); output.Attributes.SetAttribute("data-grid-pager", ""); output.Attributes.SetAttribute("data-page-size", Math.Max(1, PageSize)); output.Attributes.SetAttribute("data-page-sizes", PageSizes); output.Content.SetHtmlContent("<button type=\"button\" data-grid-page=\"first\" aria-label=\"Primeira página\">«</button><button type=\"button\" data-grid-page=\"prev\" aria-label=\"Página anterior\">‹</button><span data-grid-page-info></span><button type=\"button\" data-grid-page=\"next\" aria-label=\"Próxima página\">›</button><button type=\"button\" data-grid-page=\"last\" aria-label=\"Última página\">»</button><label>Itens <select data-grid-page-size></select></label>"); }
}

[HtmlTargetElement("orizon-grid-context-menu", ParentTag = "orizon-enterprise-grid")]
public sealed class OrizonGridContextMenuTagHelper : TagHelper
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output) { var content = await output.GetChildContentAsync(); output.TagName = "div"; output.TagMode = TagMode.StartTagAndEndTag; output.Attributes.SetAttribute("class", "orizon-grid-context-menu"); output.Attributes.SetAttribute("role", "menu"); output.Attributes.SetAttribute("data-grid-context-menu", ""); output.Attributes.SetAttribute("hidden", "hidden"); output.Content.SetHtmlContent(content.GetContent()); }
}

[HtmlTargetElement("orizon-grid-selection", ParentTag = "orizon-enterprise-grid")]
public sealed class OrizonGridSelectionTagHelper : TagHelper
{
    [HtmlAttributeName("mode")] public string Mode { get; set; } = "multiple";
    [HtmlAttributeName("checkbox")] public bool Checkbox { get; set; } = true;
    [HtmlAttributeName("cell")] public bool Cell { get; set; } = true;
    public override void Process(TagHelperContext context, TagHelperOutput output) { output.TagName = "span"; output.TagMode = TagMode.StartTagAndEndTag; output.Attributes.SetAttribute("data-grid-selection", ""); output.Attributes.SetAttribute("data-mode", Mode); output.Attributes.SetAttribute("data-checkbox", Checkbox.ToString().ToLowerInvariant()); output.Attributes.SetAttribute("data-cell", Cell.ToString().ToLowerInvariant()); output.Attributes.SetAttribute("hidden", "hidden"); }
}
