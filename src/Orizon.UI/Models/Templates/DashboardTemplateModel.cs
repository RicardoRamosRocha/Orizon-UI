using System.Collections.ObjectModel;
using Orizon.UI.Models.Dashboard;

namespace Orizon.UI.Models.Templates;

/// <summary>
/// Represents a complete dashboard assembled from reusable layout sections and widgets.
/// </summary>
public sealed class DashboardTemplateModel
{
    public string Name { get; set; } = string.Empty;

    public string? DisplayName { get; set; }

    public string? Category { get; set; }

    public string? Title { get; set; }

    public string? Subtitle { get; set; }

    public string? Description { get; set; }

    public string? Version { get; set; }

    public string? Author { get; set; }

    public string? PreviewImage { get; set; }

    public Collection<string> Tags { get; } = [];

    public Collection<string> Widgets { get; } = [];

    public string? LayoutName { get; set; }

    public DashboardLayoutModel Layout { get; set; } = new();

    public Collection<DashboardSectionModel> Sections => Layout.Sections;

    public IDictionary<string, object?> Metadata { get; } =
        new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);
}
