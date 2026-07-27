using Orizon.UI.Enums.Templates;

namespace Orizon.UI.Models.Templates;

/// <summary>
/// Holds optional, behavior-free configuration for a dashboard template.
/// </summary>
public sealed class DashboardTemplateOptions
{
    public DashboardTheme? Theme { get; set; }
    public bool? Fluid { get; set; }
    public bool? ShowHeader { get; set; }
    public bool? ShowFooter { get; set; }
    public bool? EnableSidebar { get; set; }
    public bool? EnableToolbar { get; set; }
    public bool? EnableBreadcrumb { get; set; }
    public string? Density { get; set; }
    public string? Animation { get; set; }
}
