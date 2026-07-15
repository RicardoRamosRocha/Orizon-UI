using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orizon.UI.TagHelpers;

[HtmlTargetElement("orizon-button")]
public sealed class OrizonButtonTagHelper : TagHelper
{
    [HtmlAttributeName("variant")]
    public string Variant { get; set; } = "primary";

    [HtmlAttributeName("type")]
    public string Type { get; set; } = "button";

    [HtmlAttributeName("class")]
    public string? CssClass { get; set; }

    public override void Process(TagHelperContext context,
                                 TagHelperOutput output)
    {
        output.TagName = "button";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("type", Type);

        output.Attributes.SetAttribute(
            "class",
            BuildClass());
    }

    private string BuildClass()
    {
        var css = "orizon-button";

        switch (Variant.ToLowerInvariant())
        {
            case "secondary":
                css += " orizon-button-secondary";
                break;

            case "danger":
                css += " orizon-button-danger";
                break;
        }

        if (!string.IsNullOrWhiteSpace(CssClass))
            css += $" {CssClass}";

        return css;
    }
}