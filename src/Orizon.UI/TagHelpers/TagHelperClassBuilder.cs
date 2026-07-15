namespace Orizon.UI.TagHelpers;

internal static class TagHelperClassBuilder
{
    public static string Build(params string?[] classes)
    {
        return string.Join(
            " ",
            classes.Where(cssClass => !string.IsNullOrWhiteSpace(cssClass)));
    }
}