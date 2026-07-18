using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orizon.UI.TagHelpers;

[HtmlTargetElement("orizon-textarea")]
public sealed class OrizonTextareaTagHelper : TagHelper
{
    [HtmlAttributeName("asp-for")] public ModelExpression? For { get; set; }
    [HtmlAttributeName("label")] public string? Label { get; set; }
    [HtmlAttributeName("name")] public string? Name { get; set; }
    [HtmlAttributeName("id")] public string? Id { get; set; }
    [HtmlAttributeName("value")] public string? Value { get; set; }
    [HtmlAttributeName("placeholder")] public string? Placeholder { get; set; }
    [HtmlAttributeName("hint")] public string? Hint { get; set; }
    [HtmlAttributeName("helper-text")] public string? HelperText { get; set; }
    [HtmlAttributeName("error")] public string? Error { get; set; }
    [HtmlAttributeName("required")] public bool Required { get; set; }
    [HtmlAttributeName("disabled")] public bool Disabled { get; set; }
    [HtmlAttributeName("readonly")] public bool Readonly { get; set; }
    [HtmlAttributeName("autofocus")] public bool Autofocus { get; set; }
    [HtmlAttributeName("tabindex")] public int? TabIndex { get; set; }
    [HtmlAttributeName("rows")] public int Rows { get; set; } = 4;
    [HtmlAttributeName("maxlength")] public int? MaxLength { get; set; }
    [HtmlAttributeName("counter")] public bool Counter { get; set; }
    [HtmlAttributeName("auto-resize")] public bool AutoResize { get; set; } = true;
    [HtmlAttributeName("aria-label")] public string? AriaLabel { get; set; }
    [HtmlAttributeName("aria-describedby")] public string? AriaDescribedBy { get; set; }
    [HtmlAttributeName("tooltip")] public string? Tooltip { get; set; }
    [HtmlAttributeName("loading")] public bool Loading { get; set; }
    [HtmlAttributeName("success")] public bool Success { get; set; }
    [HtmlAttributeName("invalid")] public bool Invalid { get; set; }
    [ViewContext, HtmlAttributeNotBound] public ViewContext ViewContext { get; set; } = null!;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var e = HtmlEncoder.Default; var child = await output.GetChildContentAsync(); var name = For is null ? Name ?? $"orizon-textarea-{context.UniqueId}" : ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(For.Name); var id = Id ?? TagBuilder.CreateSanitizedId(name, "_"); var value = Value ?? For?.Model?.ToString() ?? child.GetContent(); var error = Error; if (For is not null && ViewContext.ViewData.ModelState.TryGetValue(name, out var state)) { value = state.AttemptedValue ?? value; error ??= state.Errors.FirstOrDefault()?.ErrorMessage; }
        var required = Required || For?.Metadata.ValidatorMetadata.OfType<RequiredAttribute>().Any() == true; var helper = HelperText ?? Hint; var bad = Invalid || !string.IsNullOrWhiteSpace(error); var described = string.Join(" ", new[] { helper is null ? null : $"{id}-hint", bad ? $"{id}-error" : null, AriaDescribedBy }.Where(x => !string.IsNullOrWhiteSpace(x))); var cls = output.Attributes["class"]?.Value?.ToString(); output.Attributes.Clear(); output.TagName = "div"; output.TagMode = TagMode.StartTagAndEndTag; output.Attributes.SetAttribute("class", TagHelperClassBuilder.Build("orizon-advanced-field", "orizon-advanced-field--textarea", bad ? "is-invalid" : null, Success ? "is-success" : null, Loading ? "is-loading" : null, cls));
        output.Content.SetHtmlContent($"{FormFieldMarkup.Label(id, Label ?? For?.Metadata.DisplayName, required)}<div class=\"orizon-advanced-field__control\"{(Tooltip is null ? "" : $" title=\"{e.Encode(Tooltip)}\"")}><textarea class=\"orizon-advanced-field__input orizon-advanced-field__textarea\" id=\"{e.Encode(id)}\" name=\"{e.Encode(name)}\" rows=\"{Math.Max(1, Rows)}\"{Attr("placeholder", Placeholder)}{Bool("required", required)}{Bool("disabled", Disabled)}{Bool("readonly", Readonly)}{Bool("autofocus", Autofocus)}{Attr("tabindex", TabIndex?.ToString())}{Attr("maxlength", MaxLength?.ToString())}{Attr("aria-label", AriaLabel)}{Attr("aria-describedby", described)} aria-invalid=\"{bad.ToString().ToLowerInvariant()}\"{(AutoResize ? " data-textarea-autoresize" : "")}>{e.Encode(value)}</textarea>{(Loading ? "<span class=\"orizon-advanced-field__spinner\"></span>" : "")}</div>{(Counter ? $"<span class=\"orizon-advanced-field__counter\" data-field-counter>{value.Length}/{MaxLength?.ToString() ?? "∞"}</span>" : "")}{FormFieldMarkup.Message(id, helper, false)}{FormFieldMarkup.Message(id, error, true)}");
    }
    private static string Attr(string n, string? v) => string.IsNullOrWhiteSpace(v) ? "" : $" {n}=\"{HtmlEncoder.Default.Encode(v)}\""; private static string Bool(string n, bool v) => v ? $" {n}" : "";
}
