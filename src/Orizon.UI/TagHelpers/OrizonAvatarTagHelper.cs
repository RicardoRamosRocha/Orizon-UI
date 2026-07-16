using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orizon.UI.TagHelpers;

[HtmlTargetElement("orizon-avatar")]
public sealed class OrizonAvatarTagHelper : TagHelper
{
    [HtmlAttributeName("name")]
    public string Name { get; set; } = "Usuario";

    [HtmlAttributeName("src")]
    public string? Src { get; set; }

    [HtmlAttributeName("size")]
    public string Size { get; set; } = "md";

    [HtmlAttributeName("status")]
    public string? Status { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var existingClass = output.Attributes["class"]?.Value?.ToString();
        var initials = string.Join("", Name.Split(' ', StringSplitOptions.RemoveEmptyEntries).Take(2).Select(part => part[0])).ToUpperInvariant();

        output.TagName = "span";
        output.TagMode = TagMode.StartTagAndEndTag;
        output.Attributes.RemoveAll("class");
        output.Attributes.SetAttribute(
            "class",
            TagHelperClassBuilder.Build("orizon-avatar", $"orizon-avatar--{Size}", !string.IsNullOrWhiteSpace(Status) ? $"orizon-avatar--{Status}" : null, existingClass));
        output.Attributes.SetAttribute("aria-label", Name);

        var media = string.IsNullOrWhiteSpace(Src)
            ? $"""<span class="orizon-avatar__initials" aria-hidden="true">{HtmlEncoder.Default.Encode(initials)}</span>"""
            : $"""<img class="orizon-avatar__image" src="{HtmlEncoder.Default.Encode(Src)}" alt="" aria-hidden="true" />""";
        var status = string.IsNullOrWhiteSpace(Status)
            ? string.Empty
            : $"""<span class="orizon-avatar__status" aria-hidden="true"></span>""";

        output.Content.SetHtmlContent($"{media}{status}");
    }
}
