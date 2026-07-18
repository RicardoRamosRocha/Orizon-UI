using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orizon.UI.TagHelpers;

[HtmlTargetElement("orizon-button")]
public sealed class OrizonButtonTagHelper : TagHelper
{
    private static readonly HashSet<string> Styles = new(StringComparer.OrdinalIgnoreCase)
        { "solid", "soft", "outline", "ghost", "gradient", "glass", "link" };
    private static readonly HashSet<string> Colors = new(StringComparer.OrdinalIgnoreCase)
        { "primary", "secondary", "success", "warning", "danger", "info", "neutral" };

    [HtmlAttributeName("text")] public string? Text { get; set; }
    [HtmlAttributeName("variant")] public string Variant { get; set; } = "primary";
    [HtmlAttributeName("color")] public string? Color { get; set; }
    [HtmlAttributeName("size")] public string Size { get; set; } = "md";
    [HtmlAttributeName("type")] public string Type { get; set; } = "button";
    [HtmlAttributeName("href")] public string? Href { get; set; }
    [HtmlAttributeName("icon")] public string? Icon { get; set; }
    [HtmlAttributeName("icon-position")] public string IconPosition { get; set; } = "start";
    [HtmlAttributeName("block")] public bool Block { get; set; }
    [HtmlAttributeName("full-width")] public bool FullWidth { get; set; }
    [HtmlAttributeName("disabled")] public bool Disabled { get; set; }
    [HtmlAttributeName("loading")] public bool Loading { get; set; }
    [HtmlAttributeName("aria-label")] public string? AriaLabel { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var child = await output.GetChildContentAsync();
        var content = !string.IsNullOrWhiteSpace(Text) ? HtmlEncoder.Default.Encode(Text) : child.GetContent();
        var legacyColor = Colors.Contains(Variant) && !Styles.Contains(Variant) ? Variant : null;
        var style = legacyColor is null ? Normalize(Variant, Styles, "solid") : "solid";
        var color = Normalize(Color ?? legacyColor ?? "primary", Colors, "primary");
        var existingClass = output.Attributes["class"]?.Value?.ToString();
        output.Attributes.RemoveAll("class");
        output.Attributes.SetAttribute("class", TagHelperClassBuilder.Build(
            "orizon-button", $"orizon-button--{style}", $"orizon-button--{color}",
            $"orizon-button--{Normalize(Size, new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "xs", "sm", "md", "lg" }, "md")}",
            Block || FullWidth ? "orizon-button--block" : null, Loading ? "is-loading" : null, existingClass));

        if (!string.IsNullOrWhiteSpace(AriaLabel)) output.Attributes.SetAttribute("aria-label", AriaLabel);
        if (!string.IsNullOrWhiteSpace(Href))
        {
            output.TagName = "a"; output.Attributes.SetAttribute("href", Href);
            if (Disabled || Loading) { output.Attributes.SetAttribute("aria-disabled", "true"); output.Attributes.SetAttribute("tabindex", "-1"); }
        }
        else
        {
            output.TagName = "button"; output.Attributes.SetAttribute("type", Type);
            if (Disabled || Loading) output.Attributes.SetAttribute("disabled", "disabled");
        }
        output.TagMode = TagMode.StartTagAndEndTag;
        if (Loading) output.Attributes.SetAttribute("aria-busy", "true");
        var icon = string.IsNullOrWhiteSpace(Icon) ? "" : $"<i class=\"orizon-button__icon ph ph-{HtmlEncoder.Default.Encode(Icon.Replace("ph ph-", "", StringComparison.OrdinalIgnoreCase))}\" aria-hidden=\"true\"></i>";
        var spinner = Loading ? "<span class=\"orizon-button__spinner\" aria-hidden=\"true\"></span>" : "";
        output.Content.SetHtmlContent(IconPosition.Equals("end", StringComparison.OrdinalIgnoreCase)
            ? $"{spinner}<span class=\"orizon-button__content\">{content}</span>{icon}"
            : $"{spinner}{icon}<span class=\"orizon-button__content\">{content}</span>");
    }

    private static string Normalize(string value, HashSet<string> allowed, string fallback) =>
        allowed.Contains(value) ? value.ToLowerInvariant() : fallback;
}
