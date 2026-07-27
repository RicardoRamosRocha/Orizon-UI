namespace Orizon.UI.Models.Widgets;

/// <summary>
/// Identifies a model that can participate in widget composition.
/// </summary>
public interface IWidgetModel
{
    /// <summary>
    /// Gets the widget's unique HTML identifier.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets the current presentation state.
    /// </summary>
    WidgetState State { get; }

    /// <summary>
    /// Gets a value indicating whether the widget is visible.
    /// </summary>
    bool Visible { get; }
}
