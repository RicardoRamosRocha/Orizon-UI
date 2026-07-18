using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orizon.UI.TagHelpers;

[HtmlTargetElement("orizon-select")]
public sealed class OrizonSelectTagHelper : TagHelper
{
    [HtmlAttributeName("asp-for")] public ModelExpression? For { get; set; }
    [HtmlAttributeName("items")] public IEnumerable<SelectListItem>? Items { get; set; }
    [HtmlAttributeName("label")] public string? Label { get; set; }
    [HtmlAttributeName("name")] public string? Name { get; set; }
    [HtmlAttributeName("id")] public string? Id { get; set; }
    [HtmlAttributeName("placeholder")] public string? Placeholder { get; set; }
    [HtmlAttributeName("hint")] public string? Hint { get; set; }
    [HtmlAttributeName("helper-text")] public string? HelperText { get; set; }
    [HtmlAttributeName("error")] public string? Error { get; set; }
    [HtmlAttributeName("required")] public bool Required { get; set; }
    [HtmlAttributeName("disabled")] public bool Disabled { get; set; }
    [HtmlAttributeName("readonly")] public bool Readonly { get; set; }
    [HtmlAttributeName("autofocus")] public bool Autofocus { get; set; }
    [HtmlAttributeName("tabindex")] public int? TabIndex { get; set; }
    [HtmlAttributeName("multiple")] public bool Multiple { get; set; }
    [HtmlAttributeName("searchable")] public bool Searchable { get; set; }
    [HtmlAttributeName("loading")] public bool Loading { get; set; }
    [HtmlAttributeName("success")] public bool Success { get; set; }
    [HtmlAttributeName("invalid")] public bool Invalid { get; set; }
    [HtmlAttributeName("aria-label")] public string? AriaLabel { get; set; }
    [HtmlAttributeName("aria-describedby")] public string? AriaDescribedBy { get; set; }
    [HtmlAttributeName("tooltip")] public string? Tooltip { get; set; }
    [ViewContext, HtmlAttributeNotBound] public ViewContext ViewContext { get; set; } = null!;
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var e = HtmlEncoder.Default; var child = await output.GetChildContentAsync(); var name = For is null ? Name ?? $"orizon-select-{context.UniqueId}" : ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(For.Name); var id = Id ?? TagBuilder.CreateSanitizedId(name, "_"); var selected = For?.Model?.ToString(); var error = Error; if (For is not null && ViewContext.ViewData.ModelState.TryGetValue(name, out var state)) { selected = state.AttemptedValue ?? selected; error ??= state.Errors.FirstOrDefault()?.ErrorMessage; }
        var required = Required || For?.Metadata.ValidatorMetadata.OfType<RequiredAttribute>().Any() == true; var helper = HelperText ?? Hint; var bad = Invalid || error is not null; var options = new List<string>(); if (Placeholder is not null) options.Add($"<option value=\"\"{(required ? " disabled" : "")}{(string.IsNullOrEmpty(selected) ? " selected" : "")}>{e.Encode(Placeholder)}</option>"); if (Items is not null) foreach (var item in Items) { var sel = item.Selected || item.Value == selected; options.Add($"<option value=\"{e.Encode(item.Value ?? item.Text)}\"{(sel ? " selected" : "")}{(item.Disabled ? " disabled" : "")}>{e.Encode(item.Text)}</option>"); }
        options.Add(child.GetContent()); var described = string.Join(" ", new[] { helper is null ? null : $"{id}-hint", bad ? $"{id}-error" : null, AriaDescribedBy }.Where(x => !string.IsNullOrWhiteSpace(x))); var cls = output.Attributes["class"]?.Value?.ToString(); output.Attributes.Clear(); output.TagName = "div"; output.TagMode = TagMode.StartTagAndEndTag; output.Attributes.SetAttribute("class", TagHelperClassBuilder.Build("orizon-advanced-field", "orizon-advanced-field--select", bad ? "is-invalid" : null, Success ? "is-success" : null, Loading ? "is-loading" : null, cls)); output.Content.SetHtmlContent($"{FormFieldMarkup.Label(id, Label ?? For?.Metadata.DisplayName, required)}<div class=\"orizon-advanced-field__control\"{(Tooltip is null ? "" : $" title=\"{e.Encode(Tooltip)}\"")}><select class=\"orizon-advanced-field__input\" id=\"{e.Encode(id)}\" name=\"{e.Encode(name)}\"{B("required", required)}{B("disabled", Disabled || Readonly)}{B("multiple", Multiple)}{B("autofocus", Autofocus)}{A("tabindex", TabIndex?.ToString())}{A("aria-label", AriaLabel)}{A("aria-describedby", described)} aria-invalid=\"{bad.ToString().ToLowerInvariant()}\" data-select-searchable=\"{Searchable.ToString().ToLowerInvariant()}\">{string.Join("", options)}</select>{(Loading ? "<span class=\"orizon-advanced-field__spinner\"></span>" : "")}</div>{FormFieldMarkup.Message(id, helper, false)}{FormFieldMarkup.Message(id, error, true)}");
    }
    private static string A(string n, string? v) => string.IsNullOrWhiteSpace(v) ? "" : $" {n}=\"{HtmlEncoder.Default.Encode(v)}\""; private static string B(string n, bool v) => v ? $" {n}" : "";
}
