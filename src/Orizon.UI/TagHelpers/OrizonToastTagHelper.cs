using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orizon.UI.TagHelpers;

[HtmlTargetElement("orizon-toast")]
public sealed class OrizonToastTagHelper : TagHelper
{
    [HtmlAttributeName("variant")]
    public string Variant { get; set; } = "info";

    [HtmlAttributeName("title")]
    public string? Title { get; set; }

    [HtmlAttributeName("dismissible")]
    public bool Dismissible { get; set; } = true;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var content = await output.GetChildContentAsync();
        var existingClass = output.Attributes["class"]?.Value?.ToString();

        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;
        output.Attributes.RemoveAll("class");
        output.Attributes.SetAttribute("class", TagHelperClassBuilder.Build("orizon-toast", $"orizon-toast--{Variant}", existingClass));
        output.Attributes.SetAttribute("role", "status");
        output.Attributes.SetAttribute("aria-live", "polite");
        output.Attributes.SetAttribute("data-orizon-toast", "");

        var title = string.IsNullOrWhiteSpace(Title)
            ? string.Empty
            : $"""<div class="orizon-toast__title">{Title}</div>""";
        var close = Dismissible
            ? """<button class="orizon-toast__close" type="button" data-orizon-toast-close aria-label="Fechar notificacao"><span aria-hidden="true">&times;</span></button>"""
            : string.Empty;

        output.Content.SetHtmlContent(
            $"""
            <div class="orizon-toast__content">
                {title}
                <div class="orizon-toast__message">{content.GetContent()}</div>
            </div>
            {close}
            """);
    }
}
