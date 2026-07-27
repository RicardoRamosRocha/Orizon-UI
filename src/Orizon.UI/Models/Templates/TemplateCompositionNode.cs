using System.Collections.ObjectModel;

namespace Orizon.UI.Models.Templates;

/// <summary>
/// Represents one manifest and its explicitly registered derived templates.
/// </summary>
public sealed class TemplateCompositionNode
{
    public required DashboardTemplateManifest Template { get; init; }
    public Collection<TemplateCompositionNode> DerivedTemplates { get; } = [];
}
