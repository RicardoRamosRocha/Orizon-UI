using System.Collections.ObjectModel;
using Orizon.UI.Enums.Templates;
using Orizon.UI.Models.Widgets;

namespace Orizon.UI.Models.Dashboard;

/// <summary>
/// Groups related widgets in a responsive dashboard region.
/// </summary>
public sealed class DashboardSectionModel
{
    public string? Name { get; set; }
    public TemplateRegion Region { get; set; } = TemplateRegion.Content;
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DashboardColumns Columns { get; set; } = DashboardColumns.Auto;
    public Collection<IWidgetModel> Widgets { get; } = [];
}
