using Orizon.UI.Models.Dashboard;
using Orizon.UI.Models.Templates;
using Orizon.UI.Models.Widgets;
using Orizon.UI.Models.Widgets.Dashboard.ActivityFeed;
using Orizon.UI.Models.Widgets.Dashboard.DashboardHero;
using Orizon.UI.Models.Widgets.Dashboard.Kpi;
using Orizon.UI.Models.Widgets.Dashboard.QuickActions;

namespace Orizon.UI.Builders;

/// <summary>
/// Builds reusable dashboard templates by composing the existing dashboard infrastructure.
/// </summary>
public sealed class DashboardTemplateBuilder
{
    private readonly DashboardTemplateModel _template = new();
    private DashboardSectionModel? _sharedContentSection;

    public DashboardTemplateBuilder Named(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        _template.Name = name;
        return this;
    }

    public DashboardTemplateBuilder Catalog(
        string? displayName = null,
        string? category = null,
        string? version = null,
        string? author = null,
        string? previewImage = null)
    {
        _template.DisplayName = displayName;
        _template.Category = category;
        _template.Version = version;
        _template.Author = author;
        _template.PreviewImage = previewImage;
        return this;
    }

    public DashboardTemplateBuilder Tags(params string[] tags)
    {
        ArgumentNullException.ThrowIfNull(tags);
        AddDistinct(_template.Tags, tags);
        return this;
    }

    public DashboardTemplateBuilder UsesWidgets(params string[] widgets)
    {
        ArgumentNullException.ThrowIfNull(widgets);
        AddDistinct(_template.Widgets, widgets);
        return this;
    }

    public DashboardTemplateBuilder LayoutName(string? layoutName)
    {
        _template.LayoutName = layoutName;
        return this;
    }

    public DashboardTemplateBuilder Header(
        string? title,
        string? subtitle = null,
        string? description = null)
    {
        _template.Title = title;
        _template.Subtitle = subtitle;
        _template.Description = description;
        return this;
    }

    public DashboardTemplateBuilder Layout(Action<DashboardLayoutModel> configure)
    {
        ArgumentNullException.ThrowIfNull(configure);
        configure(_template.Layout);
        return this;
    }

    public DashboardTemplateBuilder Hero(DashboardHeroWidgetModel hero)
    {
        ArgumentNullException.ThrowIfNull(hero);
        AddDistinct(_template.Widgets, ["Dashboard Hero"]);
        return AddWidgetSection(DashboardColumns.One, hero);
    }

    public DashboardTemplateBuilder Kpis(params KpiWidgetModel[] kpis)
    {
        ArgumentNullException.ThrowIfNull(kpis);
        AddDistinct(_template.Widgets, ["KPI"]);
        return AddWidgetSection(GetColumns(kpis.Length), kpis);
    }

    public DashboardTemplateBuilder QuickActions(QuickActionsWidgetModel quickActions)
    {
        ArgumentNullException.ThrowIfNull(quickActions);
        AddDistinct(_template.Widgets, ["Quick Actions"]);
        return AddWidgetToSharedSection(quickActions);
    }

    public DashboardTemplateBuilder ActivityFeed(ActivityFeedWidgetModel activityFeed)
    {
        ArgumentNullException.ThrowIfNull(activityFeed);
        AddDistinct(_template.Widgets, ["Activity Feed"]);
        return AddWidgetToSharedSection(activityFeed);
    }

    public DashboardTemplateBuilder Section(DashboardSectionModel section)
    {
        ArgumentNullException.ThrowIfNull(section);
        _template.Sections.Add(section);
        return this;
    }

    public DashboardTemplateBuilder Metadata(string key, object? value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key);
        _template.Metadata[key] = value;
        return this;
    }

    public DashboardTemplateModel Build()
    {
        if (string.IsNullOrWhiteSpace(_template.Name))
        {
            throw new InvalidOperationException("A dashboard template must have a name.");
        }

        _template.Layout.Title = _template.Title;
        _template.Layout.Subtitle = _template.Subtitle;
        _template.DisplayName ??= _template.Title ?? _template.Name;
        _template.LayoutName ??= nameof(DashboardLayoutModel);
        return _template;
    }

    private DashboardTemplateBuilder AddWidgetSection(
        DashboardColumns columns,
        params WidgetModelBase[] widgets)
    {
        var section = new DashboardSectionModel
        {
            Columns = columns
        };

        foreach (var widget in widgets)
        {
            ArgumentNullException.ThrowIfNull(widget);
            section.Widgets.Add(widget);
        }

        _template.Sections.Add(section);
        return this;
    }

    private DashboardTemplateBuilder AddWidgetToSharedSection(WidgetModelBase widget)
    {
        if (_sharedContentSection is null)
        {
            _sharedContentSection = new DashboardSectionModel
            {
                Title = DefaultContentSectionTitle,
                Columns = DashboardColumns.Two
            };
            _template.Sections.Add(_sharedContentSection);
        }

        _sharedContentSection.Widgets.Add(widget);
        return this;
    }

    private static DashboardColumns GetColumns(int widgetCount)
    {
        return widgetCount switch
        {
            1 => DashboardColumns.One,
            2 => DashboardColumns.Two,
            3 => DashboardColumns.Three,
            4 => DashboardColumns.Four,
            _ => DashboardColumns.Auto
        };
    }

    private static void AddDistinct(
        ICollection<string> destination,
        IEnumerable<string> values)
    {
        foreach (var value in values.Where(value => !string.IsNullOrWhiteSpace(value)))
        {
            if (!destination.Contains(value, StringComparer.OrdinalIgnoreCase))
            {
                destination.Add(value);
            }
        }
    }

    private const string DefaultContentSectionTitle = "Visão geral";
}
