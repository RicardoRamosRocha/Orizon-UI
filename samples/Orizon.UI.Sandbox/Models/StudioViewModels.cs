namespace Orizon.UI.Sandbox.Models;

using Orizon.UI.Models.Templates;
using Orizon.UI.Models.Layout;

public sealed record StudioSearchItem(string Title, string Kind, string Url, string Keywords = "");

public sealed record StudioIconItem(string Name, string Category);

public sealed record StudioComponentDocumentation(
    string Slug,
    string Name,
    string Description,
    string Razor,
    string Html,
    string Css,
    string CSharp,
    IReadOnlyList<string> Anatomy,
    IReadOnlyList<string> Variations,
    IReadOnlyList<string> States,
    IReadOnlyDictionary<string, string> Properties,
    IReadOnlyList<string> Accessibility);

public sealed class StudioPageViewModel
{
    public required string Section { get; init; }
    public IReadOnlyList<StudioSearchItem> SearchItems { get; init; } = [];
    public IReadOnlyList<StudioComponentDocumentation> Components { get; init; } = [];
    public StudioComponentDocumentation? Component { get; init; }
    public IReadOnlyList<StudioIconItem> Icons { get; init; } = [];
}

public sealed class TemplateCompositionPageViewModel
{
    public required DashboardTemplateModel BaseTemplate { get; init; }
    public required DashboardTemplateModel DerivedTemplate { get; init; }
    public required DashboardTemplateManifest BaseManifest { get; init; }
    public required DashboardTemplateManifest DerivedManifest { get; init; }
    public required IReadOnlyCollection<TemplateCompositionNode> CompositionTree { get; init; }
}

public sealed class ResponsiveLayoutExplorerViewModel
{
    public required DashboardTemplateModel Template { get; init; }
    public required ResponsiveBreakpoint CurrentBreakpoint { get; init; }
    public required IReadOnlyCollection<ResponsiveBreakpoint> SupportedBreakpoints { get; init; }
}

public sealed record ResponsiveDashboardPreview(
    ResponsiveBreakpoint Breakpoint,
    DashboardTemplateModel Template);

public sealed class ResponsiveDashboardEngineViewModel
{
    public IReadOnlyCollection<ResponsiveDashboardPreview> Previews { get; init; } = [];
}
