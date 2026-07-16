using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orizon.UI.TagHelpers;

[HtmlTargetElement("orizon-pagination")]
public sealed class OrizonPaginationTagHelper : TagHelper
{
    [HtmlAttributeName("page")]
    public int Page { get; set; } = 1;

    [HtmlAttributeName("total-pages")]
    public int TotalPages { get; set; } = 1;

    [HtmlAttributeName("href-template")]
    public string HrefTemplate { get; set; } = "?page={0}";

    [HtmlAttributeName("label")]
    public string Label { get; set; } = "Paginacao";

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var existingClass = output.Attributes["class"]?.Value?.ToString();
        var current = Math.Clamp(Page, 1, Math.Max(TotalPages, 1));
        var total = Math.Max(TotalPages, 1);

        output.TagName = "nav";
        output.TagMode = TagMode.StartTagAndEndTag;
        output.Attributes.RemoveAll("class");
        output.Attributes.SetAttribute("class", TagHelperClassBuilder.Build("orizon-pagination", existingClass));
        output.Attributes.SetAttribute("aria-label", Label);

        var items = new List<string>
        {
            PageLink(current - 1, "Anterior", current == 1, false)
        };

        for (var page = 1; page <= total; page++)
        {
            items.Add(PageLink(page, page.ToString(), false, page == current));
        }

        items.Add(PageLink(current + 1, "Proxima", current == total, false));

        output.Content.SetHtmlContent(
            $"""
            <ul class="orizon-pagination__list">
                {string.Join(Environment.NewLine, items)}
            </ul>
            """);
    }

    private string PageLink(int page, string label, bool disabled, bool current)
    {
        var encodedLabel = HtmlEncoder.Default.Encode(label);

        if (disabled)
        {
            return $"""<li><span class="orizon-pagination__item is-disabled" aria-disabled="true">{encodedLabel}</span></li>""";
        }

        var href = HtmlEncoder.Default.Encode(string.Format(HrefTemplate, page));

        return
            $"""
            <li>
                <a class="orizon-pagination__item{(current ? " is-current" : string.Empty)}" href="{href}" {(current ? "aria-current=\"page\"" : string.Empty)}>{encodedLabel}</a>
            </li>
            """;
    }
}
