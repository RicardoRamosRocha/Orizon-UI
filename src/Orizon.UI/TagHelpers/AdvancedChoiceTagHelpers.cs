using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orizon.UI.TagHelpers;

[HtmlTargetElement("orizon-multi-select")]
public sealed class OrizonMultiSelectTagHelper : TagHelper
{
    [HtmlAttributeName("asp-for")] public ModelExpression? For { get; set; }
    [HtmlAttributeName("items")] public IEnumerable<SelectListItem>? Items { get; set; }
    [HtmlAttributeName("name")] public string? Name { get; set; }
    [HtmlAttributeName("id")] public string? Id { get; set; }
    [HtmlAttributeName("label")] public string? Label { get; set; }
    [HtmlAttributeName("placeholder")] public string Placeholder { get; set; } = "Selecione...";
    [HtmlAttributeName("helper-text")] public string? HelperText { get; set; }
    [HtmlAttributeName("required")] public bool Required { get; set; }
    [HtmlAttributeName("disabled")] public bool Disabled { get; set; }
    [HtmlAttributeName("readonly")] public bool Readonly { get; set; }
    [HtmlAttributeName("searchable")] public bool Searchable { get; set; } = true;
    [HtmlAttributeName("select-all")] public bool SelectAll { get; set; } = true;
    [HtmlAttributeName("clearable")] public bool Clearable { get; set; } = true;
    [ViewContext, HtmlAttributeNotBound] public ViewContext ViewContext { get; set; } = null!;
    public override void Process(TagHelperContext context, TagHelperOutput output) { var e = HtmlEncoder.Default; var name = For is null ? Name ?? $"orizon-multi-{context.UniqueId}" : ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(For.Name); var id = Id ?? TagBuilder.CreateSanitizedId(name, "_"); var selected = (For?.Model as System.Collections.IEnumerable)?.Cast<object>().Select(x => x?.ToString()).ToHashSet() ?? new HashSet<string?>(); var items = (Items ?? []).ToList(); output.TagName = "div"; output.TagMode = TagMode.StartTagAndEndTag; output.Attributes.SetAttribute("class", "orizon-advanced-field orizon-multi-select"); output.Attributes.SetAttribute("data-orizon-multi-select", ""); output.Content.SetHtmlContent($"{FormFieldMarkup.Label(id, Label ?? For?.Metadata.DisplayName, Required)}<button class=\"orizon-multi-select__trigger\" id=\"{id}\" type=\"button\" aria-haspopup=\"listbox\" aria-expanded=\"false\"{(Disabled || Readonly ? " disabled" : "")}><span data-multi-placeholder>{e.Encode(Placeholder)}</span><span aria-hidden=\"true\">⌄</span></button><div class=\"orizon-multi-select__panel\" hidden>{(Searchable ? "<input type=\"search\" data-multi-search placeholder=\"Pesquisar...\" aria-label=\"Pesquisar opções\">" : "")}<div class=\"orizon-multi-select__actions\">{(SelectAll ? "<button type=\"button\" data-multi-all>Selecionar tudo</button>" : "")}{(Clearable ? "<button type=\"button\" data-multi-clear>Limpar</button>" : "")}</div><div role=\"listbox\" aria-multiselectable=\"true\">{string.Join("", items.Select((x, i) => $"<label role=\"option\" aria-selected=\"{(x.Selected || selected.Contains(x.Value)).ToString().ToLowerInvariant()}\"><input type=\"checkbox\" name=\"{e.Encode(name)}\" value=\"{e.Encode(x.Value ?? x.Text)}\"{(x.Selected || selected.Contains(x.Value) ? " checked" : "")}{(x.Disabled ? " disabled" : "")}><span>{e.Encode(x.Text)}</span></label>"))}</div></div><div class=\"orizon-multi-select__chips\" data-multi-chips></div>{FormFieldMarkup.Message(id, HelperText, false)}"); }
}

[HtmlTargetElement("orizon-auto-complete")]
public sealed class OrizonAutoCompleteTagHelper : AdvancedFormFieldTagHelper
{
    [HtmlAttributeName("items")] public IEnumerable<string>? Items { get; set; }
    [HtmlAttributeName("debounce")] public int Debounce { get; set; } = 300;
    [HtmlAttributeName("min-length")] public int MinLength { get; set; } = 1;
    protected override string InputType => "text"; protected override string Kind => "autocomplete";
    protected override string AdditionalAttributes() => $" role=\"combobox\" aria-autocomplete=\"list\" aria-expanded=\"false\" data-autocomplete-input data-debounce=\"{Math.Clamp(Debounce, 0, 5000)}\" data-min-length=\"{Math.Max(0, MinLength)}\" data-items=\"{HtmlEncoder.Default.Encode(System.Text.Json.JsonSerializer.Serialize(Items ?? []))}\"";
    public override void Process(TagHelperContext context, TagHelperOutput output) { base.Process(context, output); output.PostContent.SetHtmlContent("<div class=\"orizon-autocomplete__results\" role=\"listbox\" hidden></div>"); }
}

[HtmlTargetElement("orizon-color-picker")]
public sealed class OrizonColorPickerTagHelper : AdvancedFormFieldTagHelper
{
    [HtmlAttributeName("format")] public string Format { get; set; } = "hex";
    protected override string InputType => "color"; protected override string Kind => "color";
    protected override string AdditionalAttributes() => $" data-color-picker data-color-format=\"{Format}\"";
}

[HtmlTargetElement("orizon-rating")]
public sealed class OrizonRatingTagHelper : TagHelper
{
    [HtmlAttributeName("asp-for")] public ModelExpression? For { get; set; }
    [HtmlAttributeName("name")] public string? Name { get; set; }
    [HtmlAttributeName("id")] public string? Id { get; set; }
    [HtmlAttributeName("label")] public string? Label { get; set; }
    [HtmlAttributeName("value")] public int Value { get; set; }
    [HtmlAttributeName("max")] public int Max { get; set; } = 5; [HtmlAttributeName("required")] public bool Required { get; set; }
    [HtmlAttributeName("disabled")] public bool Disabled { get; set; }
    [HtmlAttributeName("readonly")] public bool Readonly { get; set; }
    [HtmlAttributeName("helper-text")] public string? HelperText { get; set; }
    [ViewContext, HtmlAttributeNotBound] public ViewContext ViewContext { get; set; } = null!;
    public override void Process(TagHelperContext context, TagHelperOutput output) { var e = HtmlEncoder.Default; var name = For is null ? Name ?? $"rating-{context.UniqueId}" : ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(For.Name); var id = Id ?? TagBuilder.CreateSanitizedId(name, "_"); var value = For?.Model is null ? Value : Convert.ToInt32(For.Model); var max = Math.Clamp(Max, 1, 10); output.TagName = "fieldset"; output.TagMode = TagMode.StartTagAndEndTag; output.Attributes.SetAttribute("class", "orizon-rating"); output.Attributes.SetAttribute("data-orizon-rating", ""); if (Disabled || Readonly) output.Attributes.SetAttribute("aria-disabled", "true"); output.Content.SetHtmlContent($"<legend>{e.Encode(Label ?? For?.Metadata.DisplayName ?? "Avaliação")}{(Required ? " *" : "")}</legend><div class=\"orizon-rating__stars\">{string.Join("", Enumerable.Range(1, max).Select(i => $"<button type=\"button\" data-rating-value=\"{i}\" aria-label=\"{i} de {max} estrelas\" aria-pressed=\"{(i <= value).ToString().ToLowerInvariant()}\"{(Disabled || Readonly ? " disabled" : "")}><span aria-hidden=\"true\">★</span></button>"))}</div><input type=\"hidden\" id=\"{e.Encode(id)}\" name=\"{e.Encode(name)}\" value=\"{value}\" data-rating-input>{FormFieldMarkup.Message(id, HelperText, false)}"); }
}
