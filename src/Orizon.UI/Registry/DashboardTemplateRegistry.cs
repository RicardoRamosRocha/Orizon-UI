using System.Collections.ObjectModel;
using Orizon.UI.Models.Templates;
using Orizon.UI.Templates.Dashboard;

namespace Orizon.UI.Registry;

/// <summary>
/// Stores explicitly registered dashboard template factories.
/// </summary>
public sealed class DashboardTemplateRegistry
{
    private readonly Dictionary<string, Func<DashboardTemplateModel>> _templates =
        new(StringComparer.OrdinalIgnoreCase);

    public DashboardTemplateRegistry()
    {
        Register(DefaultDashboardTemplate.Name, DefaultDashboardTemplate.Create);
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
}
