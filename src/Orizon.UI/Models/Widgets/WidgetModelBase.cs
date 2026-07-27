namespace Orizon.UI.Models.Widgets;

/// <summary>
/// Provides the common presentation state shared by all widget models.
/// </summary>
public abstract class WidgetModelBase : IWidgetModel
{
    /// <summary>
    /// Gets or sets the widget's unique HTML identifier.
    /// </summary>
    public string Id { get; set; } = $"orz-widget-{Guid.NewGuid():N}";

    /// <summary>
    /// Gets or sets the widget title.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the widget subtitle.
    /// </summary>
    public string? Subtitle { get; set; }

    /// <summary>
    /// Gets or sets additional CSS classes applied to the widget.
    /// </summary>
    public string? CssClass { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the widget is visible.
    /// </summary>
    public bool Visible { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether the widget is loading.
    /// </summary>
    /// <remarks>
    /// This compatibility property is an adapter over <see cref="State"/>.
    /// Setting it to <see langword="true"/> selects <see cref="WidgetState.Loading"/>.
    /// Setting it to <see langword="false"/> restores <see cref="WidgetState.Normal"/>
    /// only when the current state is <see cref="WidgetState.Loading"/>.
    /// </remarks>
    public bool Loading
    {
        get => State == WidgetState.Loading;
        set
        {
            if (value)
            {
                State = WidgetState.Loading;
            }
            else if (State == WidgetState.Loading)
            {
                State = WidgetState.Normal;
            }
        }
    }

    /// <summary>
    /// Gets or sets the widget size.
    /// </summary>
    public WidgetSize Size { get; set; } = WidgetSize.Medium;

    /// <summary>
    /// Gets or sets the widget theme.
    /// </summary>
    public WidgetTheme Theme { get; set; } = WidgetTheme.Default;

    /// <summary>
    /// Gets or sets the widget state, which is the authoritative widget state contract.
    /// </summary>
    /// <remarks>
    /// Consumers should prefer this property when representing loading, empty, error,
    /// disabled, and normal states. The legacy <see cref="Loading"/> property reflects
    /// this value and cannot diverge from it.
    /// </remarks>
    public WidgetState State { get; set; } = WidgetState.Normal;
}
