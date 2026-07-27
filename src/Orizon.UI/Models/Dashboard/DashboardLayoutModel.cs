using System.Collections.ObjectModel;

namespace Orizon.UI.Models.Dashboard;

/// <summary>
/// Represents a complete, reusable dashboard composition.
/// </summary>
public sealed class DashboardLayoutModel
{
    public string? Title { get; set; }
    public string? Subtitle { get; set; }
    public bool ShowHeader { get; set; } = true;
    public bool ShowFooter { get; set; }
    public bool Fluid { get; set; }
    public DashboardSpacing Spacing { get; set; } = DashboardSpacing.Default;
    public Collection<DashboardSectionModel> Sections { get; } = [];
}
