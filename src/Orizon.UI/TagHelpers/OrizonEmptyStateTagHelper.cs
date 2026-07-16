using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orizon.UI.TagHelpers;

[HtmlTargetElement("orizon-empty-state")]
public sealed class OrizonEmptyStateTagHelper : TagHelper
{
    [HtmlAttributeName("title")]
    public string Title { get; set; } = "Nada por aqui";

    [HtmlAttributeName("description")]
    public string? Description { get; set; }

    [HtmlAttributeName("icon")]
    public string? Icon { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var content = await output.GetChildContentAsync();
        var existingClass = output.Attributes["class"]?.Value?.ToString();

        output.TagName = "section";
        output.TagMode = TagMode.StartTagAndEndTag;
        output.Attributes.RemoveAll("class");
        output.Attributes.SetAttribute("class", TagHelperClassBuilder.Build("orizon-empty-state", existingClass));

        var icon = string.IsNullOrWhiteSpace(Icon)
            ? string.Empty
            : $"""<div class="orizon-empty-state__icon"><i class="{Icon}" aria-hidden="true"></i></div>""";
        var description = string.IsNullOrWhiteSpace(Description)
            ? string.Empty
            : $"""<p class="orizon-empty-state__description">{Description}</p>""";

        output.Content.SetHtmlContent(
            $"""
            {icon}
            <h2 class="orizon-empty-state__title">{Title}</h2>
            {description}
            <div class="orizon-empty-state__actions">
                {content.GetContent()}
            </div>
            """);
    }
}
