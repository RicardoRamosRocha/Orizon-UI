using System.Collections.ObjectModel;
using Orizon.UI.Models.Templates;
using Orizon.UI.Templates.Dashboard;
using Orizon.UI.Factories.Templates;
using Orizon.UI.Templates.SDK;
using Orizon.UI.Models.Layout;

namespace Orizon.UI.Registry;

/// <summary>
/// Stores explicitly registered dashboard template factories.
/// </summary>
public sealed class DashboardTemplateRegistry
{
    private readonly Dictionary<string, Func<DashboardTemplateModel>> _templates =
        new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, ITemplateFactory> _factories =
        new(StringComparer.OrdinalIgnoreCase);

    public DashboardTemplateRegistry()
    {
        Register(new DefaultDashboardTemplateFactory());
        Register(new ExecutiveDashboardFactory());
        Register(new OperationsDashboardFactory());
        Register(new AnalyticsDashboardFactory());
        Register(new WorkspaceDashboardFactory());
        Register(new BlankDashboardFactory());
        Register(new ERPDashboardFactory());
    }

    public IReadOnlyCollection<string> Names =>
        new ReadOnlyCollection<string>(_templates.Keys.Order().ToArray());

    public IReadOnlyCollection<DashboardTemplateModel> GetAll()
    {
        return new ReadOnlyCollection<DashboardTemplateModel>(
            _templates.Values.Select(factory => factory())
                .OrderBy(template => template.DisplayName ?? template.Name)
                .ToArray());
    }

    public DashboardTemplateModel? GetByName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        return _templates.TryGetValue(name, out var factory) ? factory() : null;
    }

    public bool Contains(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        return _templates.ContainsKey(name);
    }

    public IReadOnlyCollection<string> GetCategories()
    {
        return new ReadOnlyCollection<string>(
            GetAll()
                .Select(template => template.Category)
                .Where(category => !string.IsNullOrWhiteSpace(category))
                .Select(category => category!)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .Order(StringComparer.OrdinalIgnoreCase)
                .ToArray());
    }

    public IReadOnlyCollection<DashboardTemplateModel> GetTemplatesByCategory(string category)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(category);
        return new ReadOnlyCollection<DashboardTemplateModel>(
            GetAll()
                .Where(template => string.Equals(
                    template.Category,
                    category,
                    StringComparison.OrdinalIgnoreCase))
                .ToArray());
    }

    public void Register(string name, Func<DashboardTemplateModel> factory)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(factory);
        _templates[name] = factory;
    }

    public void Register(ITemplateFactory factory)
    {
        ArgumentNullException.ThrowIfNull(factory);
        var manifest = factory.GetManifest();
        ArgumentException.ThrowIfNullOrWhiteSpace(manifest.Name);
        _factories[manifest.Name] = factory;
        Register(manifest.Name, factory.Create);
    }

    public DashboardTemplateManifest? GetManifest(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        if (_factories.TryGetValue(name, out var factory))
        {
            return factory.GetManifest();
        }

        return GetByName(name) is { } template
            ? template.Manifest ?? DashboardTemplateManifest.FromModel(template)
            : null;
    }

    public IReadOnlyCollection<DashboardTemplateManifest> GetManifests()
    {
        return new ReadOnlyCollection<DashboardTemplateManifest>(
            Names.Select(GetManifest)
                .Where(manifest => manifest is not null)
                .Select(manifest => manifest!)
                .OrderBy(manifest => manifest.DisplayName ?? manifest.Name)
                .ToArray());
    }

    public IReadOnlyCollection<ITemplateFactory> GetFactories()
    {
        return new ReadOnlyCollection<ITemplateFactory>(
            _factories.Values.OrderBy(factory =>
                factory.GetManifest().DisplayName ?? factory.GetManifest().Name).ToArray());
    }

    public IReadOnlyCollection<DashboardTemplateManifest> GetFeatured() =>
        FilterManifests(manifest => manifest.Featured == true);

    public IReadOnlyCollection<DashboardTemplateManifest> GetExperimental() =>
        FilterManifests(manifest => manifest.Experimental == true);

    public IReadOnlyCollection<DashboardTemplateManifest> GetDeprecated() =>
        FilterManifests(manifest => manifest.Deprecated == true);

    public IReadOnlyCollection<DashboardTemplateManifest> GetDerivedTemplates(string baseTemplate)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(baseTemplate);
        return new ReadOnlyCollection<DashboardTemplateManifest>(
            GetManifests()
                .Where(manifest => string.Equals(
                    manifest.BaseTemplate,
                    baseTemplate,
                    StringComparison.OrdinalIgnoreCase))
                .OrderBy(manifest => manifest.DisplayName ?? manifest.Name)
                .ToArray());
    }

    public DashboardTemplateManifest? GetBaseTemplate(string derivedTemplate)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(derivedTemplate);
        var manifest = GetManifest(derivedTemplate);
        return string.IsNullOrWhiteSpace(manifest?.BaseTemplate)
            ? null
            : GetManifest(manifest.BaseTemplate);
    }

    public IReadOnlyCollection<TemplateCompositionNode> GetCompositionTree()
    {
        var manifests = GetManifests();
        var roots = manifests
            .Where(manifest => string.IsNullOrWhiteSpace(manifest.BaseTemplate))
            .Select(manifest => CreateCompositionNode(manifest, manifests))
            .Where(node => node.DerivedTemplates.Count > 0 || SupportsComposition(node.Template.Name))
            .ToArray();
        return new ReadOnlyCollection<TemplateCompositionNode>(roots);
    }

    public bool SupportsComposition(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        var manifest = GetManifest(name);
        return manifest?.SupportsComposition == true || manifest?.Composable == true;
    }

    public IReadOnlyCollection<DashboardTemplateManifest> GetResponsiveTemplates()
    {
        return new ReadOnlyCollection<DashboardTemplateManifest>(
            GetManifests()
                .Where(manifest => manifest.SupportsResponsiveLayout == true)
                .OrderBy(manifest => manifest.DisplayName ?? manifest.Name)
                .ToArray());
    }

    public bool SupportsResponsive(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        return GetManifest(name)?.SupportsResponsiveLayout == true;
    }

    public IReadOnlyCollection<ResponsiveBreakpoint> GetSupportedBreakpoints(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        var manifest = GetManifest(name)
            ?? throw new KeyNotFoundException($"Dashboard template '{name}' is not registered.");
        return new ReadOnlyCollection<ResponsiveBreakpoint>(
            manifest.SupportedBreakpoints.ToArray());
    }

    public bool TryCreate(string name, out DashboardTemplateModel? template)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        if (_templates.TryGetValue(name, out var factory))
        {
            template = factory();
            return true;
        }

        template = null;
        return false;
    }

    public DashboardTemplateModel Create(string name)
    {
        return GetByName(name)
            ?? throw new KeyNotFoundException($"Dashboard template '{name}' is not registered.");
    }

    private IReadOnlyCollection<DashboardTemplateManifest> FilterManifests(
        Func<DashboardTemplateManifest, bool> predicate)
    {
        return new ReadOnlyCollection<DashboardTemplateManifest>(
            GetManifests().Where(predicate).ToArray());
    }

    private static TemplateCompositionNode CreateCompositionNode(
        DashboardTemplateManifest manifest,
        IReadOnlyCollection<DashboardTemplateManifest> manifests)
    {
        var node = new TemplateCompositionNode { Template = manifest };
        foreach (var derived in manifests
            .Where(candidate => string.Equals(
                candidate.BaseTemplate,
                manifest.Name,
                StringComparison.OrdinalIgnoreCase))
            .OrderBy(candidate => candidate.DisplayName ?? candidate.Name))
        {
            node.DerivedTemplates.Add(CreateCompositionNode(derived, manifests));
        }
        return node;
    }
}
