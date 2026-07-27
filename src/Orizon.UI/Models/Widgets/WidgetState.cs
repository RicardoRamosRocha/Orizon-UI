namespace Orizon.UI.Models.Widgets;

/// <summary>
/// Defines the current state of a widget.
/// </summary>
public enum WidgetState
{
    /// <summary>
    /// The widget is operating normally.
    /// </summary>
    Normal,

    /// <summary>
    /// The widget is loading content.
    /// </summary>
    Loading,

    /// <summary>
    /// The widget has no content to display.
    /// </summary>
    Empty,

    /// <summary>
    /// The widget encountered an error.
    /// </summary>
    Error,

    /// <summary>
    /// The widget is disabled.
    /// </summary>
    Disabled
}
