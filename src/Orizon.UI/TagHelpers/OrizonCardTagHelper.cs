using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orizon.UI.TagHelpers;

[HtmlTargetElement("orizon-card")]
public sealed class OrizonCardTagHelper : TagHelper
{
    [HtmlAttributeName("title")]
    public string? Title { get; set; }

    [HtmlAttributeName("subtitle")]
    public string? Subtitle { get; set; }

    [HtmlAttributeName("icon")]
    public string? Icon { get; set; }

    [HtmlAttributeName("href")]
    public string? Href { get; set; }

    [HtmlAttributeName("variant")]
    public string Variant { get; set; } = "default";

    [HtmlAttributeName("padding")]
    public string Padding { get; set; } = "md";

    [HtmlAttributeName("hoverable")]
    public bool Hoverable { get; set; }

    public override async Task ProcessAsync(
        TagHelperContext context,
        TagHelperOutput output)
    {
        var content = await output.GetChildContentAsync();
        var existingClass = output.Attributes["class"]?.Value?.ToString();

        output.TagName = string.IsNullOrWhiteSpace(Href) ? "section" : "a";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.RemoveAll("class");

        output.Attributes.SetAttribute(
            "class",
            TagHelperClassBuilder.Build(
                "orizon-card",
                $"orizon-card--{Variant}",
                $"orizon-card--padding-{Padding}",
                Hoverable || !string.IsNullOrWhiteSpace(Href)
                    ? "orizon-card--hoverable"
                    : null,
                existingClass));

        if (!string.IsNullOrWhiteSpace(Href))
        {
            output.Attributes.SetAttribute("href", Href);
        }

        var hasHeader =
            !string.IsNullOrWhiteSpace(Title) ||
            !string.IsNullOrWhiteSpace(Subtitle) ||
            !string.IsNullOrWhiteSpace(Icon);

        var iconMarkup = string.IsNullOrWhiteSpace(Icon)
            ? string.Empty
            : $"""
               <div class="orizon-card__icon">
                   <i class="{Icon}" aria-hidden="true"></i>
               </div>
               """;

        var titleMarkup = string.IsNullOrWhiteSpace(Title)
            ? string.Empty
            : $"""<h3 class="orizon-card__title">{Title}</h3>""";

        var subtitleMarkup = string.IsNullOrWhiteSpace(Subtitle)
            ? string.Empty
            : $"""<p class="orizon-card__subtitle">{Subtitle}</p>""";

        var headerMarkup = hasHeader
            ? $"""
               <header class="orizon-card__header">
                   {iconMarkup}

                   <div class="orizon-card__heading">
                       {titleMarkup}
                       {subtitleMarkup}
                   </div>
               </header>
               """
            : string.Empty;

        output.Content.SetHtmlContent(
            $"""
            {headerMarkup}

            <div class="orizon-card__body">
                {content.GetContent()}
            </div>
            """);
    }
}