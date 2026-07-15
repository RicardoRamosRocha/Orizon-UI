using System.Text.Encodings.Web;

namespace Orizon.UI.TagHelpers;

internal static class FormFieldMarkup
{
    public static string Label(string fieldId, string? label, bool required)
    {
        if (string.IsNullOrWhiteSpace(label))
        {
            return string.Empty;
        }

        var requiredMarkup = required
            ? """<span class="orizon-field__required" aria-hidden="true">*</span>"""
            : string.Empty;

        return
            $"""
            <label class="orizon-field__label" for="{Encode(fieldId)}">
                {Encode(label)}
                {requiredMarkup}
            </label>
            """;
    }

    public static string Message(string fieldId, string? message, bool isError)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return string.Empty;
        }

        var id = isError ? $"{fieldId}-error" : $"{fieldId}-hint";
        var modifier = isError ? " orizon-field__message--error" : string.Empty;
        var role = isError ? " role=\"alert\"" : string.Empty;

        return
            $"""
            <p class="orizon-field__message{modifier}" id="{Encode(id)}"{role}>
                {Encode(message)}
            </p>
            """;
    }

    public static string BuildDescribedBy(string fieldId, string? hint, string? error)
    {
        var ids = new List<string>();

        if (!string.IsNullOrWhiteSpace(hint))
        {
            ids.Add($"{fieldId}-hint");
        }

        if (!string.IsNullOrWhiteSpace(error))
        {
            ids.Add($"{fieldId}-error");
        }

        return string.Join(" ", ids);
    }

    public static string OptionalAttribute(string name, string? value)
    {
        return string.IsNullOrWhiteSpace(value)
            ? string.Empty
            : $"{name}=\"{Encode(value)}\"";
    }

    public static string BooleanAttribute(string name, bool value)
    {
        return value ? name : string.Empty;
    }

    private static string Encode(string value)
    {
        return HtmlEncoder.Default.Encode(value);
    }
}
