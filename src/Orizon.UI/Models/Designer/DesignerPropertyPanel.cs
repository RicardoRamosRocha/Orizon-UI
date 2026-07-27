using Orizon.UI.Models.Templates;

namespace Orizon.UI.Models.Designer;

/// <summary>Dynamic property sources for the current model and selection.</summary>
public sealed class DesignerPropertyPanel
{
    public DashboardTemplateManifest? Manifest { get; internal set; }
    public DesignerWidget? Widget { get; internal set; }
    public object? Placement => Widget?.Placement;
    public object? Responsive { get; internal set; }
    public TemplateComposition? Composition { get; internal set; }
}
