namespace Orizon.UI.Models.Widgets.Dashboard.Kpi;

/// <summary>
/// Represents a key performance indicator.
/// </summary>
public sealed class KpiWidgetModel : WidgetModelBase
{
    public string? Value { get; set; }

    public string? Description { get; set; }

    public string? Trend { get; set; }

    public string TrendType { get; set; } = "neutral";

    public string? Icon { get; set; }

    public string? Footer { get; set; }
}
