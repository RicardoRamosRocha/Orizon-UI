namespace Orizon.UI.Models.Layout;

/// <summary>
/// Provides optional, presentation-agnostic rules for a layout zone or breakpoint.
/// </summary>
public sealed class ResponsiveLayoutOptions
{
    public int? Columns { get; set; }
    public int? Rows { get; set; }
    public int? Gap { get; set; }
    public int? Span { get; set; }
    public int? Priority { get; set; }
    public bool? Hidden { get; set; }
    public bool? Collapsed { get; set; }
    public int? Order { get; set; }
    public int? MinWidth { get; set; }
    public int? MaxWidth { get; set; }
    public bool? Stack { get; set; }
}
