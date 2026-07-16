using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orizon.UI.TagHelpers;

[HtmlTargetElement("orizon-table")]
public sealed class OrizonTableTagHelper : TagHelper
{
    [HtmlAttributeName("label")]
    public string? Label { get; set; }

    [HtmlAttributeName("striped")]
    public bool Striped { get; set; }

    [HtmlAttributeName("hoverable")]
    public bool Hoverable { get; set; }

    [HtmlAttributeName("compact")]
    public bool Compact { get; set; }

    [HtmlAttributeName("bordered")]
    public bool Bordered { get; set; }

    public override async Task ProcessAsync(
        TagHelperContext context,
        TagHelperOutput output)
    {
        var childContent = await output.GetChildContentAsync();
        var existingClass = output.Attributes["class"]?.Value?.ToString();
        var label = string.IsNullOrWhiteSpace(Label)
            ? "Tabela de dados"
            : Label;

        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;
        output.Attributes.RemoveAll("class");
        output.Attributes.SetAttribute("class", "orizon-table-container orizon-scrollbar");
        output.Attributes.SetAttribute("role", "region");
        output.Attributes.SetAttribute("tabindex", "0");
        output.Attributes.SetAttribute("aria-label", label);

        output.Content.SetHtmlContent(
            $"""
            <table class="{TagHelperClassBuilder.Build(
                "orizon-table",
                Striped ? "orizon-table--striped" : null,
                Hoverable ? "orizon-table--hoverable" : null,
                Compact ? "orizon-table--compact" : null,
                Bordered ? "orizon-table--bordered" : null,
                existingClass)}">
                {childContent.GetContent()}
            </table>
            """);
    }
}
