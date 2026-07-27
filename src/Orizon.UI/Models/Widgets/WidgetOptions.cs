namespace Orizon.UI.Models.Widgets;

/// <summary>
/// Defines optional capabilities exposed by a widget.
/// </summary>
public sealed class WidgetOptions
{
    /// <summary>
    /// Gets or sets a value indicating whether the widget can be collapsed.
    /// </summary>
    public bool Collapsible { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the widget can be refreshed.
    /// </summary>
    public bool Refreshable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the widget can be closed.
    /// </summary>
    public bool Closable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the widget can enter full-screen mode.
    /// </summary>
    public bool FullScreen { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the widget header is shown.
    /// </summary>
    public bool ShowHeader { get; set; } = true;
}
