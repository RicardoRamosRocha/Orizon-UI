using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orizon.UI.TagHelpers;

public abstract class AdvancedFormFieldTagHelper : TagHelper
{
    [HtmlAttributeName("asp-for")] public ModelExpression? For { get; set; }
    [HtmlAttributeName("name")] public string? Name { get; set; }
    [HtmlAttributeName("id")] public string? Id { get; set; }
    [HtmlAttributeName("value")] public string? Value { get; set; }
    [HtmlAttributeName("label")] public string? Label { get; set; }
    [HtmlAttributeName("placeholder")] public string? Placeholder { get; set; }
    [HtmlAttributeName("helper-text")] public string? HelperText { get; set; }
    [HtmlAttributeName("required")] public bool Required { get; set; }
    [HtmlAttributeName("disabled")] public bool Disabled { get; set; }
    [HtmlAttributeName("readonly")] public bool Readonly { get; set; }
    [HtmlAttributeName("autofocus")] public bool Autofocus { get; set; }
    [HtmlAttributeName("tabindex")] public int? TabIndex { get; set; }
    [HtmlAttributeName("style")] public string? FieldStyle { get; set; }
    [HtmlAttributeName("aria-label")] public string? AriaLabel { get; set; }
    [HtmlAttributeName("aria-describedby")] public string? AriaDescribedBy { get; set; }
    [HtmlAttributeName("tooltip")] public string? Tooltip { get; set; }
    [HtmlAttributeName("icon")] public string? Icon { get; set; }
    [HtmlAttributeName("prefix")] public string? Prefix { get; set; }
    [HtmlAttributeName("suffix")] public string? Suffix { get; set; }
    [HtmlAttributeName("loading")] public bool Loading { get; set; }
    [HtmlAttributeName("success")] public bool Success { get; set; }
    [HtmlAttributeName("invalid")] public bool Invalid { get; set; }
    [HtmlAttributeName("validation")] public bool Validation { get; set; } = true;
    [HtmlAttributeName("clearable")] public bool Clearable { get; set; }
    [HtmlAttributeName("maxlength")] public int? MaxLength { get; set; }
    [HtmlAttributeName("counter")] public bool Counter { get; set; }
    [ViewContext, HtmlAttributeNotBound] public ViewContext ViewContext { get; set; } = null!;

    protected abstract string InputType { get; }
    protected virtual string Kind => InputType;
    protected virtual string AdditionalAttributes() => string.Empty;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var e = HtmlEncoder.Default; var binding = ResolveBinding(context); var invalid = Invalid || binding.Error is not null; var described = string.Join(" ", new[] { HelperText is null ? null : $"{binding.Id}-help", invalid ? $"{binding.Id}-error" : null, AriaDescribedBy }.Where(x => !string.IsNullOrWhiteSpace(x)));
        var cls = output.Attributes["class"]?.Value?.ToString(); output.Attributes.Clear(); output.TagName = "div"; output.TagMode = TagMode.StartTagAndEndTag; output.Attributes.SetAttribute("class", TagHelperClassBuilder.Build("orizon-advanced-field", $"orizon-advanced-field--{Kind}", invalid ? "is-invalid" : null, Success ? "is-success" : null, Loading ? "is-loading" : null, Disabled ? "is-disabled" : null, cls)); if (!string.IsNullOrWhiteSpace(FieldStyle)) output.Attributes.SetAttribute("style", FieldStyle);
        var adornStart = $"{(Icon is null ? "" : $"<span class=\"orizon-advanced-field__icon\" aria-hidden=\"true\"><i class=\"ph ph-{e.Encode(Icon)}\"></i></span>")}{(Prefix is null ? "" : $"<span class=\"orizon-advanced-field__prefix\">{e.Encode(Prefix)}</span>")}"; var adornEnd = $"{(Suffix is null ? "" : $"<span class=\"orizon-advanced-field__suffix\">{e.Encode(Suffix)}</span>")}{(Loading ? "<span class=\"orizon-advanced-field__spinner\" aria-hidden=\"true\"></span>" : "")}{(Clearable ? "<button class=\"orizon-advanced-field__action\" type=\"button\" data-field-clear aria-label=\"Limpar campo\">&times;</button>" : "")}";
        output.Content.SetHtmlContent($"{FormFieldMarkup.Label(binding.Id, Label ?? For?.Metadata.DisplayName, binding.Required)}<div class=\"orizon-advanced-field__control\"{(Tooltip is null ? "" : $" title=\"{e.Encode(Tooltip)}\"")}>{adornStart}<input class=\"orizon-advanced-field__input\" id=\"{e.Encode(binding.Id)}\" name=\"{e.Encode(binding.Name)}\" type=\"{InputType}\" value=\"{e.Encode(binding.Value)}\"{Attr("placeholder", Placeholder)}{Bool("required", binding.Required)}{Bool("disabled", Disabled)}{Bool("readonly", Readonly)}{Bool("autofocus", Autofocus)}{Attr("tabindex", TabIndex?.ToString())}{Attr("aria-label", AriaLabel)}{Attr("aria-describedby", described)} aria-invalid=\"{invalid.ToString().ToLowerInvariant()}\"{(MaxLength.HasValue ? $" maxlength=\"{MaxLength}\"" : "")}{AdditionalAttributes()} data-orizon-field=\"{Kind}\">{adornEnd}</div>{(Counter ? $"<span class=\"orizon-advanced-field__counter\" data-field-counter>{binding.Value.Length}/{MaxLength?.ToString() ?? "∞"}</span>" : "")}{FormFieldMarkup.Message(binding.Id, HelperText, false)}{FormFieldMarkup.Message(binding.Id, binding.Error, true)}");
    }

    protected (string Name, string Id, string Value, string? Error, bool Required) ResolveBinding(TagHelperContext context)
    {
        var name = For is null ? Name ?? $"orizon-field-{context.UniqueId.Replace("__", "")}" : ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(For.Name); var id = Id ?? TagBuilder.CreateSanitizedId(name, "_"); var modelValue = Value ?? Convert.ToString(For?.Model, CultureInfo.CurrentCulture) ?? ""; string? error = null;
        if (For is not null && ViewContext.ViewData.ModelState.TryGetValue(name, out var state)) { modelValue = state.AttemptedValue ?? modelValue; if (Validation) error = state.Errors.FirstOrDefault()?.ErrorMessage; }
        var required = Required || For?.Metadata.ValidatorMetadata.OfType<RequiredAttribute>().Any() == true;
        return (name, id, modelValue, error, required);
    }
    protected static string Attr(string name, string? value) => string.IsNullOrWhiteSpace(value) ? "" : $" {name}=\"{HtmlEncoder.Default.Encode(value)}\"";
    protected static string Bool(string name, bool value) => value ? $" {name}" : "";
}
