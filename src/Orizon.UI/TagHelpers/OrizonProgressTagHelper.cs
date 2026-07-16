using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orizon.UI.TagHelpers;

[HtmlTargetElement("orizon-progress")]
public sealed class OrizonProgressTagHelper : TagHelper
{
    [HtmlAttributeName("value")]
    public int Value { get; set; }

    [HtmlAttributeName("max")]
    public int Max { get; set; } = 100;

    [HtmlAttributeName("label")]
    public string? Label { get; set; }

    [HtmlAttributeName("show-label")]
    public bool ShowLabel { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var existingClass = output.Attributes["class"]?.Value?.ToString();
        var max = Math.Max(Max, 1);
        var value = Math.Clamp(Value, 0, max);
        var percent = (int)Math.Round(value * 100m / max);

        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;
        output.Attributes.RemoveAll("class");
        output.Attributes.SetAttribute("class", TagHelperClassBuilder.Build("orizon-progress", existingClass));
        output.Attributes.SetAttribute("role", "progressbar");
        output.Attributes.SetAttribute("aria-valuemin", "0");
        output.Attributes.SetAttribute("aria-valuemax", max.ToString());
        output.Attributes.SetAttribute("aria-valuenow", value.ToString());

        if (!string.IsNullOrWhiteSpace(Label))
        {
            output.Attributes.SetAttribute("aria-label", Label);
        }

        var label = ShowLabel
            ? $"""<span class="orizon-progress__label">{HtmlEncoder.Default.Encode(Label ?? "Progresso")} <strong>{percent}%</strong></span>"""
            : string.Empty;

        output.Content.SetHtmlContent(
            $"""
            {label}
            <span class="orizon-progress__track" aria-hidden="true">
                <span class="orizon-progress__bar" style="width: {percent}%"></span>
            </span>
            """);
    }
}
