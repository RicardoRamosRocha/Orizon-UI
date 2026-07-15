using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orizon.UI.TagHelpers;

[HtmlTargetElement("orizon-button")]
public sealed class OrizonButtonTagHelper : TagHelper
{
    [HtmlAttributeName("variant")]
    public string Variant { get; set; } = "primary";

    [HtmlAttributeName("size")]
    public string Size { get; set; } = "md";

    [HtmlAttributeName("type")]
    public string Type { get; set; } = "button";

    [HtmlAttributeName("href")]
    public string? Href { get; set; }

    [HtmlAttributeName("icon")]
    public string? Icon { get; set; }

    [HtmlAttributeName("icon-position")]
    public string IconPosition { get; set; } = "start";

    [HtmlAttributeName("block")]
    public bool Block { get; set; }

    [HtmlAttributeName("disabled")]
    public bool Disabled { get; set; }

    [HtmlAttributeName("loading")]
    public bool Loading { get; set; }

    [HtmlAttributeName("aria-label")]
    public string? AriaLabel { get; set; }

    public override async Task ProcessAsync(
        TagHelperContext context,
        TagHelperOutput output)
    {
        var childContent = await output.GetChildContentAsync();

        var existingClass = output.Attributes["class"]?.Value?.ToString();

        output.Attributes.RemoveAll("class");

        output.Attributes.SetAttribute(
            "class",
            TagHelperClassBuilder.Build(
                "orizon-button",
                $"orizon-button--{Variant}",
                $"orizon-button--{Size}",
                Block ? "orizon-button--block" : null,
                Loading ? "is-loading" : null,
                existingClass));

        if (!string.IsNullOrWhiteSpace(AriaLabel))
        {
            output.Attributes.SetAttribute("aria-label", AriaLabel);
        }

        if (!string.IsNullOrWhiteSpace(Href))
        {
            output.TagName = "a";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.SetAttribute("href", Href);
            output.Attributes.SetAttribute("role", "button");

            if (Disabled || Loading)
            {
                output.Attributes.SetAttribute("aria-disabled", "true");
                output.Attributes.SetAttribute("tabindex", "-1");
            }
        }
        else
        {
            output.TagName = "button";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.SetAttribute("type", Type);

            if (Disabled || Loading)
            {
                output.Attributes.SetAttribute("disabled", "disabled");
            }
        }

        var content = childContent.GetContent();

        if (Loading)
        {
            output.Attributes.SetAttribute("aria-busy", "true");

            output.Content.SetHtmlContent(
                $"""
                <span class="orizon-button__spinner" aria-hidden="true"></span>
                <span class="orizon-button__content">{content}</span>
                """);

            return;
        }

        if (string.IsNullOrWhiteSpace(Icon))
        {
            output.Content.SetHtmlContent(content);
            return;
        }

        var iconMarkup =
            $"""<i class="orizon-button__icon {Icon}" aria-hidden="true"></i>""";

        output.Content.SetHtmlContent(
            IconPosition.Equals("end", StringComparison.OrdinalIgnoreCase)
                ? $"""
                   <span class="orizon-button__content">{content}</span>
                   {iconMarkup}
                   """
                : $"""
                   {iconMarkup}
                   <span class="orizon-button__content">{content}</span>
                   """);
    }
}