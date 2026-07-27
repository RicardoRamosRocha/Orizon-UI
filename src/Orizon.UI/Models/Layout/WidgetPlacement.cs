using System.Collections.ObjectModel;
using Orizon.UI.Models.Widgets;

namespace Orizon.UI.Models.Layout;

/// <summary>
/// Describes where a widget participates in a responsive layout without CSS knowledge.
/// </summary>
public sealed class WidgetPlacement
{
    public required IWidgetModel Widget { get; set; }
    public string WidgetId => Widget.Id;
    public LayoutZone Zone { get; set; } = LayoutZone.Main;
    public string? Region { get; set; }
    public int? Order { get; set; }
    public int? Priority { get; set; }
    public int? ColumnSpan { get; set; }
    public int? RowSpan { get; set; }
    public int? MinWidth { get; set; }
    public int? MaxWidth { get; set; }
    public Collection<ResponsiveBreakpoint> VisibleOn { get; } = [];
    public Collection<ResponsiveBreakpoint> HiddenOn { get; } = [];

    public bool IsVisible(ResponsiveBreakpoint breakpoint) =>
        !HiddenOn.Contains(breakpoint) &&
        (VisibleOn.Count == 0 || VisibleOn.Contains(breakpoint));
}
