using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orizon.UI.TagHelpers;

[HtmlTargetElement("orizon-dropdown")]
public sealed class OrizonDropdownTagHelper : TagHelper
{
    [HtmlAttributeName("label")]
    public string Label { get; set; } = "Abrir menu";

    [HtmlAttributeName("align")]
    public string Align { get; set; } = "start";

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var content = await output.GetChildContentAsync();
        var menuId = $"orizon-dropdown-{Guid.NewGuid():N}";
        var existingClass = output.Attributes["class"]?.Value?.ToString();
        var encodedLabel = HtmlEncoder.Default.Encode(Label);
        var encodedMenuId = HtmlEncoder.Default.Encode(menuId);

        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;
        output.Attributes.RemoveAll("class");
        output.Attributes.SetAttribute(
            "class",
            TagHelperClassBuilder.Build("orizon-dropdown", $"orizon-dropdown--{Align}", existingClass));
        output.Attributes.SetAttribute("data-orizon-dropdown", "");

        output.Content.SetHtmlContent(
            $"""
            <button class="orizon-dropdown__trigger" type="button" data-orizon-dropdown-trigger aria-haspopup="menu" aria-expanded="false" aria-controls="{encodedMenuId}">
                <span>{encodedLabel}</span>
                <span class="orizon-dropdown__chevron" aria-hidden="true">⌄</span>
            </button>
            <div class="orizon-dropdown__menu" id="{encodedMenuId}" role="menu" data-orizon-dropdown-menu hidden>
                {content.GetContent()}
            </div>
            """);
    }
}
