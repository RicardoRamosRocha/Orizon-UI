namespace Orizon.UI.Models.Widgets;

/// <summary>
/// Describes an action exposed by a widget.
/// </summary>
public sealed class WidgetAction
{
    /// <summary>
    /// Gets or sets the action text.
    /// </summary>
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the action icon identifier.
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// Gets or sets the action destination URL.
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// Gets or sets the semantic action variant.
    /// </summary>
    public string Variant { get; set; } = "default";
}
