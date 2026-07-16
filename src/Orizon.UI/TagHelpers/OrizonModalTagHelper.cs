using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orizon.UI.TagHelpers;

[HtmlTargetElement("orizon-modal")]
public sealed class OrizonModalTagHelper : TagHelper
{
    [HtmlAttributeName("id")]
    public string? Id { get; set; }

    [HtmlAttributeName("title")]
    public string? Title { get; set; }

    [HtmlAttributeName("size")]
    public string Size { get; set; } = "md";

    [HtmlAttributeName("open")]
    public bool Open { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var content = await output.GetChildContentAsync();
        var modalId = string.IsNullOrWhiteSpace(Id) ? $"orizon-modal-{Guid.NewGuid():N}" : Id;
        var titleId = $"{modalId}-title";
        var existingClass = output.Attributes["class"]?.Value?.ToString();

        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;
        output.Attributes.RemoveAll("class");
        output.Attributes.SetAttribute(
            "class",
            TagHelperClassBuilder.Build("orizon-modal", $"orizon-modal--{Size}", Open ? "is-open" : null, existingClass));
        output.Attributes.SetAttribute("id", modalId);
        output.Attributes.SetAttribute("data-orizon-modal", "");
        output.Attributes.SetAttribute("role", "dialog");
        output.Attributes.SetAttribute("aria-modal", "true");
        output.Attributes.SetAttribute("aria-labelledby", titleId);

        if (!Open)
        {
            output.Attributes.SetAttribute("hidden", "hidden");
        }

        output.Content.SetHtmlContent(
            $"""
            <div class="orizon-modal__backdrop" data-orizon-modal-close aria-hidden="true"></div>
            <div class="orizon-modal__panel" tabindex="-1">
                <header class="orizon-modal__header">
                    <h2 class="orizon-modal__title" id="{titleId}">{Title}</h2>
                    <button class="orizon-modal__close" type="button" data-orizon-modal-close aria-label="Fechar modal">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </header>
                <div class="orizon-modal__body">
                    {content.GetContent()}
                </div>
            </div>
            """);
    }
}
