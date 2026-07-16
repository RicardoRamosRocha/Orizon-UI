using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orizon.UI.TagHelpers;

[HtmlTargetElement("orizon-textarea")]
public sealed class OrizonTextareaTagHelper : TagHelper
{
    [HtmlAttributeName("label")]
    public string? Label { get; set; }

    [HtmlAttributeName("name")]
    public string? Name { get; set; }

    [HtmlAttributeName("id")]
    public string? Id { get; set; }

    [HtmlAttributeName("value")]
    public string? Value { get; set; }

    [HtmlAttributeName("placeholder")]
    public string? Placeholder { get; set; }

    [HtmlAttributeName("hint")]
    public string? Hint { get; set; }

    [HtmlAttributeName("error")]
    public string? Error { get; set; }

    [HtmlAttributeName("required")]
    public bool Required { get; set; }

    [HtmlAttributeName("disabled")]
    public bool Disabled { get; set; }

    [HtmlAttributeName("readonly")]
    public bool Readonly { get; set; }

    [HtmlAttributeName("rows")]
    public int Rows { get; set; } = 4;

    public override async Task ProcessAsync(
        TagHelperContext context,
        TagHelperOutput output)
    {
        var childContent = await output.GetChildContentAsync();
        var fieldId = ResolveId();
        var describedBy = FormFieldMarkup.BuildDescribedBy(fieldId, Hint, Error);
        var existingClass = output.Attributes["class"]?.Value?.ToString();
        var content = !string.IsNullOrWhiteSpace(Value)
            ? Value
            : childContent.GetContent();

        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;
        output.Attributes.RemoveAll("class");
        output.Attributes.SetAttribute(
            "class",
            TagHelperClassBuilder.Build(
                "orizon-field",
                !string.IsNullOrWhiteSpace(Error) ? "has-error" : null,
                Disabled ? "is-disabled" : null,
                existingClass));

        output.Content.SetHtmlContent(
            $"""
            {FormFieldMarkup.Label(fieldId, Label, Required)}
            <textarea class="orizon-textarea"
                      id="{HtmlEncoder.Default.Encode(fieldId)}"
                      name="{HtmlEncoder.Default.Encode(Name ?? fieldId)}"
                      rows="{Rows}"
                      {FormFieldMarkup.OptionalAttribute("placeholder", Placeholder)}
                      {FormFieldMarkup.BooleanAttribute("required", Required)}
                      {FormFieldMarkup.BooleanAttribute("disabled", Disabled)}
                      {FormFieldMarkup.BooleanAttribute("readonly", Readonly)}
                      aria-required="{Required.ToString().ToLowerInvariant()}"
                      aria-invalid="{(!string.IsNullOrWhiteSpace(Error)).ToString().ToLowerInvariant()}"
                      {FormFieldMarkup.OptionalAttribute("aria-describedby", describedBy)}>{HtmlEncoder.Default.Encode(content)}</textarea>
            {FormFieldMarkup.Message(fieldId, Hint, false)}
            {FormFieldMarkup.Message(fieldId, Error, true)}
            """);
    }

    private string ResolveId()
    {
        if (!string.IsNullOrWhiteSpace(Id))
        {
            return Id;
        }

        if (!string.IsNullOrWhiteSpace(Name))
        {
            return Name.Replace(".", "_", StringComparison.Ordinal);
        }

        return $"orizon-textarea-{Guid.NewGuid():N}";
    }
}
