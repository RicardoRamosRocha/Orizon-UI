using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orizon.UI.TagHelpers;

[HtmlTargetElement("orizon-spinner")]
public sealed class OrizonSpinnerTagHelper : TagHelper
{
    [HtmlAttributeName("size")]
    public string Size { get; set; } = "md";

    [HtmlAttributeName("label")]
    public string Label { get; set; } = "Carregando";

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var existingClass = output.Attributes["class"]?.Value?.ToString();

        output.TagName = "span";
        output.TagMode = TagMode.StartTagAndEndTag;
        output.Attributes.RemoveAll("class");
        output.Attributes.SetAttribute("class", TagHelperClassBuilder.Build("orizon-spinner", $"orizon-spinner--{Size}", existingClass));
        output.Attributes.SetAttribute("role", "status");
        output.Attributes.SetAttribute("aria-label", Label);
        output.Content.SetHtmlContent("""<span class="orizon-spinner__track" aria-hidden="true"></span>""");
    }
}
