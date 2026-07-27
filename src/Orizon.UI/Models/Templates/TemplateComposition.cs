using System.Collections.ObjectModel;

namespace Orizon.UI.Models.Templates;

/// <summary>
/// Describes how a derived template was assembled from its base template.
/// </summary>
public sealed class TemplateComposition
{
    public DashboardTemplateModel? BaseTemplate { get; set; }
    public DashboardTemplateModel? DerivedTemplate { get; set; }
    public Collection<string> InheritedRegions { get; } = [];
    public Collection<string> ReplacedRegions { get; } = [];
    public Collection<string> AddedRegions { get; } = [];
    public Collection<string> RemovedRegions { get; } = [];
}
