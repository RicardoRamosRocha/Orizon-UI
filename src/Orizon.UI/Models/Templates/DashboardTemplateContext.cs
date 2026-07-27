using Orizon.UI.Enums.Templates;
using System.Collections.ObjectModel;
using Orizon.UI.Models.Layout;

namespace Orizon.UI.Models.Templates;

/// <summary>
/// Carries configuration, metadata and rendering data during template composition.
/// </summary>
public sealed class DashboardTemplateContext
{
    public DashboardTemplateModel? ParentTemplate { get; set; }
    public DashboardTemplateModel? CurrentTemplate { get; set; }
    public TemplateRegion? Region { get; set; }
    public TemplateSlot? Slot { get; set; }
    public ResponsiveBreakpoint? CurrentBreakpoint { get; set; }
    public IDictionary<ResponsiveBreakpoint, ResponsiveLayoutOptions> Layout { get; } =
        new Dictionary<ResponsiveBreakpoint, ResponsiveLayoutOptions>();
    public LayoutZoneCollection Zones { get; } = [];
    public Collection<WidgetPlacement> Placements { get; } = [];
    public DashboardTemplateOptions? Options { get; set; }
    public DashboardTemplateManifest? Manifest { get; set; }
    public IDictionary<string, object?> Metadata { get; } =
        new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);
    public IDictionary<string, object?> Rendering { get; } =
        new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);
    public IDictionary<DashboardSlot, object?> Slots { get; } =
        new Dictionary<DashboardSlot, object?>();
    public IDictionary<string, TemplateSlot> NamedSlots { get; } =
        new Dictionary<string, TemplateSlot>(StringComparer.OrdinalIgnoreCase);
}
