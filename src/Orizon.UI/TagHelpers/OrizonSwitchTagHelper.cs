using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orizon.UI.TagHelpers;

[HtmlTargetElement("orizon-switch")]
public sealed class OrizonSwitchTagHelper : TagHelper
{
    [HtmlAttributeName("asp-for")] public ModelExpression? For { get; set; }
    [HtmlAttributeName("name")] public string? Name { get; set; }
    [HtmlAttributeName("value")] public string Value { get; set; } = "true";
    [HtmlAttributeName("checked")] public bool Checked { get; set; }
    [HtmlAttributeName("label")] public string? Label { get; set; }
    [HtmlAttributeName("description")] public string? Description { get; set; }
    [HtmlAttributeName("disabled")] public bool Disabled { get; set; }
    [HtmlAttributeName("size")] public string Size { get; set; } = "md";
    [HtmlAttributeName("color")] public string Color { get; set; } = "primary";
    [HtmlAttributeName("validation-message")] public bool ValidationMessage { get; set; } = true;
    [ViewContext, HtmlAttributeNotBound] public ViewContext ViewContext { get; set; } = null!;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var e = HtmlEncoder.Default; var name = For?.Name ?? Name ?? $"orizon-switch-{Guid.NewGuid():N}"; var id = name.Replace('.', '_');
        var isChecked = For?.Model as bool? ?? Checked; var descriptionId = string.IsNullOrWhiteSpace(Description) ? null : $"{id}-description";
        var error = For is null || !ValidationMessage ? null : ViewContext.ViewData.ModelState.TryGetValue(For.Name, out ModelStateEntry? state) ? state.Errors.FirstOrDefault()?.ErrorMessage : null;
        output.TagName = "div"; output.TagMode = TagMode.StartTagAndEndTag; var cls = output.Attributes["class"]?.Value?.ToString(); output.Attributes.RemoveAll("class");
        output.Attributes.SetAttribute("class", TagHelperClassBuilder.Build("orizon-switch", $"orizon-switch--{Size}", $"orizon-switch--{Color}", Disabled ? "is-disabled" : null, error is not null ? "has-error" : null, cls));
        output.Content.SetHtmlContent($"<input type=\"hidden\" name=\"{e.Encode(name)}\" value=\"false\"{(Disabled ? " disabled" : "")}><input class=\"orizon-switch__input\" type=\"checkbox\" id=\"{e.Encode(id)}\" name=\"{e.Encode(name)}\" value=\"{e.Encode(Value)}\"{(isChecked ? " checked" : "")}{(Disabled ? " disabled" : "")}{(descriptionId is null ? "" : $" aria-describedby=\"{e.Encode(descriptionId)}\"")}><label class=\"orizon-switch__label\" for=\"{e.Encode(id)}\"><span class=\"orizon-switch__track\" aria-hidden=\"true\"><span class=\"orizon-switch__thumb\"></span></span><span class=\"orizon-switch__copy\"><span class=\"orizon-switch__title\">{e.Encode(Label ?? For?.Metadata.DisplayName ?? name)}</span>{(descriptionId is null ? "" : $"<span class=\"orizon-switch__description\" id=\"{e.Encode(descriptionId)}\">{e.Encode(Description!)}</span>")}</span></label>{(error is null ? "" : $"<span class=\"orizon-field__error\">{e.Encode(error)}</span>")}");
    }
}
