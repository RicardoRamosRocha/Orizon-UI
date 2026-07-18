using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orizon.UI.TagHelpers;

[HtmlTargetElement("orizon-chip")]
public sealed class OrizonChipTagHelper : TagHelper
{
    [HtmlAttributeName("text")] public string Text { get; set; } = string.Empty;
    [HtmlAttributeName("icon")] public string? Icon { get; set; }
    [HtmlAttributeName("color")] public string Color { get; set; } = "neutral";
    [HtmlAttributeName("variant")] public string Variant { get; set; } = "soft";
    [HtmlAttributeName("size")] public string Size { get; set; } = "md";
    [HtmlAttributeName("selected")] public bool Selected { get; set; }
    [HtmlAttributeName("selectable")] public bool Selectable { get; set; }
    [HtmlAttributeName("removable")] public bool Removable { get; set; }
    [HtmlAttributeName("disabled")] public bool Disabled { get; set; }
    [HtmlAttributeName("value")] public string? Value { get; set; }
    [HtmlAttributeName("name")] public string? Name { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var e = HtmlEncoder.Default; var cls = output.Attributes["class"]?.Value?.ToString(); output.Attributes.RemoveAll("class");
        output.TagName = "span"; output.TagMode = TagMode.StartTagAndEndTag;
        output.Attributes.SetAttribute("class", TagHelperClassBuilder.Build("orizon-chip", $"orizon-chip--{Variant}", $"orizon-chip--{Color}", $"orizon-chip--{Size}", Selected ? "is-selected" : null, Disabled ? "is-disabled" : null, cls));
        output.Attributes.SetAttribute("data-orizon-chip", ""); if (Selectable) { output.Attributes.SetAttribute("data-chip-selectable", ""); output.Attributes.SetAttribute("role", "checkbox"); output.Attributes.SetAttribute("tabindex", Disabled ? "-1" : "0"); output.Attributes.SetAttribute("aria-checked", Selected.ToString().ToLowerInvariant()); }
        if (Disabled) output.Attributes.SetAttribute("aria-disabled", "true"); if (Value is not null) output.Attributes.SetAttribute("data-value", Value); if (Name is not null) output.Attributes.SetAttribute("data-name", Name);
        var icon = string.IsNullOrWhiteSpace(Icon) ? "" : $"<i class=\"ph ph-{e.Encode(Icon)}\" aria-hidden=\"true\"></i>";
        var remove = Removable ? $"<button class=\"orizon-chip__remove\" type=\"button\" data-chip-remove aria-label=\"Remover {e.Encode(Text)}\"{(Disabled ? " disabled" : "")}><span aria-hidden=\"true\">&times;</span></button>" : "";
        var input = !string.IsNullOrWhiteSpace(Name) ? $"<input type=\"hidden\" name=\"{e.Encode(Name)}\" value=\"{e.Encode(Value ?? Text)}\"{(!Selected && Selectable ? " disabled" : "")} data-chip-input>" : "";
        output.Content.SetHtmlContent($"{icon}<span class=\"orizon-chip__text\">{e.Encode(Text)}</span>{remove}{input}");
    }
}
