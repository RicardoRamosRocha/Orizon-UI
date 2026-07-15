using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orizon.UI.TagHelpers;

[HtmlTargetElement("orizon-badge")]
public sealed class OrizonBadgeTagHelper : TagHelper
{
    [HtmlAttributeName("variant")]
    public string Variant { get; set; } = "neutral";

    [HtmlAttributeName("size")]
    public string Size { get; set; } = "md";

    [HtmlAttributeName("pill")]
    public bool Pill { get; set; }

    [HtmlAttributeName("dot")]
    public bool Dot { get; set; }

    public override async Task ProcessAsync(
        TagHelperContext context,
        TagHelperOutput output)
    {
        var content = await output.GetChildContentAsync();
        var existingClass = output.Attributes["class"]?.Value?.ToString();

        output.TagName = "span";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.RemoveAll("class");

        output.Attributes.SetAttribute(
            "class",
            TagHelperClassBuilder.Build(
                "orizon-badge",
                $"orizon-badge--{Variant}",
                $"orizon-badge--{Size}",
                Pill ? "orizon-badge--pill" : null,
                existingClass));

        if (Dot)
        {
            output.Content.SetHtmlContent(
                $"""
                <span class="orizon-badge__dot" aria-hidden="true"></span>
                <span>{content.GetContent()}</span>
                """);

            return;
        }

        output.Content.SetHtmlContent(content);
    }
}