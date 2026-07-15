using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orizon.UI.TagHelpers;

[HtmlTargetElement("orizon-alert")]
public sealed class OrizonAlertTagHelper : TagHelper
{
    [HtmlAttributeName("variant")]
    public string Variant { get; set; } = "info";

    [HtmlAttributeName("title")]
    public string? Title { get; set; }

    [HtmlAttributeName("icon")]
    public string? Icon { get; set; }

    [HtmlAttributeName("dismissible")]
    public bool Dismissible { get; set; }

    public override async Task ProcessAsync(
        TagHelperContext context,
        TagHelperOutput output)
    {
        var content = await output.GetChildContentAsync();
        var existingClass = output.Attributes["class"]?.Value?.ToString();

        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.RemoveAll("class");

        output.Attributes.SetAttribute(
            "class",
            TagHelperClassBuilder.Build(
                "orizon-alert",
                $"orizon-alert--{Variant}",
                existingClass));

        output.Attributes.SetAttribute("role", "alert");

        var iconMarkup = string.IsNullOrWhiteSpace(Icon)
            ? string.Empty
            : $"""
               <div class="orizon-alert__icon">
                   <i class="{Icon}" aria-hidden="true"></i>
               </div>
               """;

        var titleMarkup = string.IsNullOrWhiteSpace(Title)
            ? string.Empty
            : $"""<div class="orizon-alert__title">{Title}</div>""";

        var dismissMarkup = Dismissible
            ? """
              <button
                  class="orizon-alert__close"
                  type="button"
                  data-orizon-alert-dismiss
                  aria-label="Fechar alerta">
                  <span aria-hidden="true">&times;</span>
              </button>
              """
            : string.Empty;

        output.Content.SetHtmlContent(
            $"""
            {iconMarkup}

            <div class="orizon-alert__content">
                {titleMarkup}
                <div class="orizon-alert__message">
                    {content.GetContent()}
                </div>
            </div>

            {dismissMarkup}
            """);
    }
}