using Orizon.UI.Models.Dashboard;
using Orizon.UI.Models.Templates;
using Orizon.UI.Models.Widgets;
using Orizon.UI.Models.Widgets.Dashboard.ActivityFeed;
using Orizon.UI.Models.Widgets.Dashboard.DashboardHero;
using Orizon.UI.Models.Widgets.Dashboard.Kpi;
using Orizon.UI.Models.Widgets.Dashboard.QuickActions;
using Orizon.UI.Enums.Templates;
using Orizon.UI.Models.Layout;

namespace Orizon.UI.Builders;

/// <summary>
/// Builds reusable dashboard templates by composing the existing dashboard infrastructure.
/// </summary>
public sealed class DashboardTemplateBuilder
{
    private readonly DashboardTemplateModel _template = new();
    private DashboardSectionModel? _sharedContentSection;
    private DashboardTemplateModel? _baseTemplate;
    private bool _inherited;

    public DashboardTemplateBuilder UseResponsiveLayout(
        ResponsiveBreakpoint breakpoint = ResponsiveBreakpoint.Desktop)
    {
        _template.Context ??= new DashboardTemplateContext();
        _template.Context.CurrentBreakpoint = breakpoint;
        if (!_template.Context.Layout.ContainsKey(breakpoint))
        {
            _template.Context.Layout[breakpoint] = new ResponsiveLayoutOptions();
        }
        return this;
    }

    public DashboardTemplateBuilder AddZone(
        LayoutZone zone,
        string? name = null,
        Action<ResponsiveLayoutOptions>? configure = null)
    {
        _template.Context ??= new DashboardTemplateContext();
        var region = new LayoutRegion
        {
            Name = string.IsNullOrWhiteSpace(name) ? zone.ToString() : name,
            Zone = zone
        };
        configure?.Invoke(region.Options);

        var existing = _template.Context.Zones.Find(region.Name);
        if (existing is not null)
        {
            _template.Context.Zones.Remove(existing);
        }
        _template.Context.Zones.Add(region);
        return this;
    }

    public DashboardTemplateBuilder ConfigureZone(
        string name,
        Action<ResponsiveLayoutOptions> configure)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(configure);
        var zone = _template.Context?.Zones.Find(name)
            ?? throw new KeyNotFoundException($"Layout zone '{name}' was not found.");
        configure(zone.Options);
        return this;
    }

    public DashboardTemplateBuilder ConfigureZone(
        LayoutZone zone,
        Action<ResponsiveLayoutOptions> configure)
    {
        ArgumentNullException.ThrowIfNull(configure);
        var regions = _template.Context?.Zones.Find(zone)
            ?? throw new InvalidOperationException("A responsive layout must be initialized first.");
        if (regions.Count == 0)
        {
            throw new KeyNotFoundException($"Layout zone '{zone}' was not found.");
        }
        foreach (var region in regions)
        {
            configure(region.Options);
        }
        return this;
    }

    public DashboardTemplateBuilder ConfigureBreakpoint(
        ResponsiveBreakpoint breakpoint,
        Action<ResponsiveLayoutOptions> configure)
    {
        ArgumentNullException.ThrowIfNull(configure);
        _template.Context ??= new DashboardTemplateContext();
        if (!_template.Context.Layout.TryGetValue(breakpoint, out var options))
        {
            options = new ResponsiveLayoutOptions();
            _template.Context.Layout[breakpoint] = options;
        }
        configure(options);
        return this;
    }

    public DashboardTemplateBuilder ConfigureBreakpoint(
        string zoneName,
        ResponsiveBreakpoint breakpoint,
        Action<ResponsiveLayoutOptions> configure)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(zoneName);
        ArgumentNullException.ThrowIfNull(configure);
        var zone = _template.Context?.Zones.Find(zoneName)
            ?? throw new KeyNotFoundException($"Layout zone '{zoneName}' was not found.");
        if (!zone.Breakpoints.TryGetValue(breakpoint, out var options))
        {
            options = new ResponsiveLayoutOptions();
            zone.Breakpoints[breakpoint] = options;
        }
        configure(options);
        return this;
    }

    public DashboardTemplateBuilder PlaceWidget(WidgetPlacement placement)
    {
        ArgumentNullException.ThrowIfNull(placement);
        _template.Context ??= new DashboardTemplateContext();
        RemovePlacement(placement.WidgetId);
        _template.Context.Placements.Add(placement);
        return this;
    }

    public DashboardTemplateBuilder PlaceWidget(
        WidgetModelBase widget,
        LayoutZone zone,
        string? region = null,
        int? order = null,
        int? priority = null,
        int? columnSpan = null,
        int? rowSpan = null,
        int? minWidth = null,
        int? maxWidth = null,
        IEnumerable<ResponsiveBreakpoint>? visibleOn = null,
        IEnumerable<ResponsiveBreakpoint>? hiddenOn = null)
    {
        ArgumentNullException.ThrowIfNull(widget);
        var placement = new WidgetPlacement
        {
            Widget = widget,
            Zone = zone,
            Region = region,
            Order = order,
            Priority = priority,
            ColumnSpan = columnSpan,
            RowSpan = rowSpan,
            MinWidth = minWidth,
            MaxWidth = maxWidth
        };
        AddBreakpoints(placement.VisibleOn, visibleOn);
        AddBreakpoints(placement.HiddenOn, hiddenOn);
        return PlaceWidget(placement);
    }

    public DashboardTemplateBuilder MoveWidget(
        string widgetId,
        LayoutZone zone,
        int? order = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(widgetId);
        var placement = FindPlacement(widgetId);
        placement.Zone = zone;
        placement.Order = order ?? placement.Order;
        return this;
    }

    public DashboardTemplateBuilder RemoveWidget(string widgetId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(widgetId);
        RemovePlacement(widgetId);
        foreach (var section in _template.Sections)
        {
            var index = FindWidgetIndex(section, widgetId);
            if (index >= 0)
            {
                section.Widgets.RemoveAt(index);
            }
        }
        return this;
    }

    public DashboardTemplateBuilder BuildLayout()
    {
        var context = _template.Context
            ?? throw new InvalidOperationException("A responsive layout must be initialized first.");
        var breakpoint = context.CurrentBreakpoint ?? ResponsiveBreakpoint.Desktop;
        _template.Sections.Clear();

        var zoneOrder = context.Zones
            .Select((zone, index) => new
            {
                Zone = zone,
                Index = index,
                Options = ResolveOptions(zone, breakpoint, context.Layout)
            })
            .Where(item => item.Options.Hidden != true)
            .OrderBy(item => item.Options.Order ?? item.Index)
            .ThenByDescending(item => item.Options.Priority ?? 0);

        foreach (var item in zoneOrder)
        {
            var placements = context.Placements
                .Where(placement =>
                    placement.Zone == item.Zone.Zone &&
                    placement.IsVisible(breakpoint))
                .OrderBy(placement => placement.Order ?? int.MaxValue)
                .ThenByDescending(placement => placement.Priority ?? 0)
                .ToArray();

            foreach (var group in placements.GroupBy(placement =>
                string.IsNullOrWhiteSpace(placement.Region)
                    ? item.Zone.Name
                    : placement.Region!))
            {
                var section = new DashboardSectionModel
                {
                    Name = group.Key,
                    Region = ToTemplateRegion(item.Zone.Zone),
                    Title = group.Key,
                    Columns = ToDashboardColumns(item.Options.Stack == true
                        ? 1
                        : item.Options.Columns)
                };
                foreach (var placement in group)
                {
                    section.Widgets.Add(placement.Widget);
                }
                _template.Sections.Add(section);
            }
        }
        return this;
    }

    public DashboardTemplateBuilder UseBaseTemplate(DashboardTemplateModel baseTemplate)
    {
        ArgumentNullException.ThrowIfNull(baseTemplate);
        _baseTemplate = baseTemplate;
        _template.Composition ??= new TemplateComposition();
        _template.Composition.BaseTemplate = baseTemplate;
        _template.Context ??= new DashboardTemplateContext();
        _template.Context.ParentTemplate = baseTemplate;
        return this;
    }

    public DashboardTemplateBuilder Inherit()
    {
        if (_baseTemplate is null)
        {
            throw new InvalidOperationException("A base template must be selected before inheritance.");
        }

        if (_inherited)
        {
            return this;
        }

        CopyBaseTemplate(_baseTemplate);
        _inherited = true;
        return this;
    }

    public DashboardTemplateBuilder Inherit(DashboardTemplateModel baseTemplate) =>
        UseBaseTemplate(baseTemplate).Inherit();

    public DashboardTemplateBuilder ReplaceRegion(
        string name,
        DashboardSectionModel region)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(region);
        region.Name = name;

        var index = FindRegionIndex(name);
        if (index >= 0)
        {
            _template.Sections[index] = region;
            Track(_template.Composition?.ReplacedRegions, name);
            RemoveTracked(_template.Composition?.InheritedRegions, name);
        }
        else
        {
            _template.Sections.Add(region);
            Track(_template.Composition?.AddedRegions, name);
        }

        return this;
    }

    public DashboardTemplateBuilder ReplaceRegion(
        TemplateRegion region,
        DashboardSectionModel section) =>
        ReplaceRegion(region.ToString(), SetRegion(section, region));

    public DashboardTemplateBuilder AppendRegion(DashboardSectionModel region)
    {
        ArgumentNullException.ThrowIfNull(region);
        region.Name ??= region.Title ?? $"Region{_template.Sections.Count + 1}";
        _template.Sections.Add(region);
        Track(_template.Composition?.AddedRegions, region.Name);
        return this;
    }

    public DashboardTemplateBuilder AppendRegion(
        string name,
        DashboardSectionModel region)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        region.Name = name;
        return AppendRegion(region);
    }

    public DashboardTemplateBuilder AppendRegion(
        TemplateRegion region,
        DashboardSectionModel section) =>
        AppendRegion(region.ToString(), SetRegion(section, region));

    public DashboardTemplateBuilder RemoveRegion(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        var index = FindRegionIndex(name);
        if (index >= 0)
        {
            _template.Sections.RemoveAt(index);
            Track(_template.Composition?.RemovedRegions, name);
            RemoveTracked(_template.Composition?.InheritedRegions, name);
        }

        return this;
    }

    public DashboardTemplateBuilder RemoveRegion(TemplateRegion region) =>
        RemoveRegion(region.ToString());

    public DashboardTemplateBuilder ReplaceWidget(
        string regionName,
        string widgetId,
        WidgetModelBase widget)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(regionName);
        ArgumentException.ThrowIfNullOrWhiteSpace(widgetId);
        ArgumentNullException.ThrowIfNull(widget);

        var section = FindRegion(regionName);
        var existing = FindWidgetIndex(section, widgetId);
        if (existing < 0)
        {
            throw new KeyNotFoundException($"Widget '{widgetId}' was not found in region '{regionName}'.");
        }

        section.Widgets[existing] = widget;
        if (_template.Context?.Placements.FirstOrDefault(placement =>
            string.Equals(placement.WidgetId, widgetId, StringComparison.OrdinalIgnoreCase)) is { } placement)
        {
            placement.Widget = widget;
        }
        Track(_template.Composition?.ReplacedRegions, regionName);
        RemoveTracked(_template.Composition?.InheritedRegions, regionName);
        return this;
    }

    public DashboardTemplateBuilder AppendWidget(
        string regionName,
        WidgetModelBase widget)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(regionName);
        ArgumentNullException.ThrowIfNull(widget);
        FindRegion(regionName).Widgets.Add(widget);
        Track(_template.Composition?.ReplacedRegions, regionName);
        RemoveTracked(_template.Composition?.InheritedRegions, regionName);
        return this;
    }

    public DashboardTemplateBuilder ClearRegion(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        FindRegion(name).Widgets.Clear();
        if (_template.Context is not null)
        {
            foreach (var placement in _template.Context.Placements
                .Where(item => string.Equals(
                    item.Region,
                    name,
                    StringComparison.OrdinalIgnoreCase))
                .ToArray())
            {
                _template.Context.Placements.Remove(placement);
            }
        }
        Track(_template.Composition?.ReplacedRegions, name);
        RemoveTracked(_template.Composition?.InheritedRegions, name);
        return this;
    }

    public DashboardTemplateBuilder ClearRegion(TemplateRegion region) =>
        ClearRegion(region.ToString());

    public DashboardTemplateBuilder Compose()
    {
        if (_baseTemplate is not null && !_inherited)
        {
            Inherit();
        }

        _template.Composition ??= new TemplateComposition();
        _template.Composition.DerivedTemplate = _template;
        return this;
    }

    public DashboardTemplateBuilder UseManifest(DashboardTemplateManifest manifest)
    {
        ArgumentNullException.ThrowIfNull(manifest);
        _template.Manifest = manifest;
        manifest.ApplyTo(_template);
        return this;
    }

    public DashboardTemplateBuilder UseOptions(DashboardTemplateOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        _template.Options = options;
        _template.Context ??= new DashboardTemplateContext();
        _template.Context.Options = options;
        return this;
    }

    public DashboardTemplateBuilder UseContext(DashboardTemplateContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        _template.Context = context;
        _template.Options = context.Options ?? _template.Options;
        _template.Manifest = context.Manifest ?? _template.Manifest;
        return this;
    }

    public DashboardTemplateBuilder UseTheme(DashboardTheme theme)
    {
        return UseOptions(_template.Options ?? new DashboardTemplateOptions())
            .SetTheme(theme);
    }

    public DashboardTemplateBuilder UseMetadata(string key, object? value)
    {
        Metadata(key, value);
        _template.Context ??= new DashboardTemplateContext();
        _template.Context.Metadata[key] = value;
        return this;
    }

    public DashboardTemplateBuilder UseMetadata(IEnumerable<KeyValuePair<string, object?>> metadata)
    {
        ArgumentNullException.ThrowIfNull(metadata);
        foreach (var item in metadata)
        {
            UseMetadata(item.Key, item.Value);
        }

        return this;
    }

    public DashboardTemplateBuilder UseSlot(DashboardSlot slot, object? content)
    {
        _template.Context ??= new DashboardTemplateContext();
        _template.Context.Slots[slot] = content;
        AddSlotWidgets(slot.ToString(), ToTemplateRegion(slot), content);
        return this;
    }

    public DashboardTemplateBuilder UseSlot(
        string name,
        TemplateRegion region,
        params WidgetModelBase[] widgets)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(widgets);
        _template.Context ??= new DashboardTemplateContext();
        var slot = new TemplateSlot { Name = name, Region = region };
        foreach (var widget in widgets)
        {
            ArgumentNullException.ThrowIfNull(widget);
            slot.Widgets.Add(widget);
        }
        _template.Context.NamedSlots[name] = slot;
        _template.Context.Region = region;
        _template.Context.Slot = slot;
        return this;
    }

    public DashboardTemplateBuilder Header(object? content) => UseSlot(DashboardSlot.Header, content);

    public DashboardTemplateBuilder Toolbar(object? content) => UseSlot(DashboardSlot.Toolbar, content);

    public DashboardTemplateBuilder Sidebar(object? content) => UseSlot(DashboardSlot.Sidebar, content);

    public DashboardTemplateBuilder Content(object? content) => UseSlot(DashboardSlot.Content, content);

    public DashboardTemplateBuilder Footer(object? content) => UseSlot(DashboardSlot.Footer, content);

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

    public DashboardTemplateBuilder EmptySection(
        string? title = null,
        string? description = null,
        DashboardColumns columns = DashboardColumns.Auto)
    {
        _template.Sections.Add(new DashboardSectionModel
        {
            Title = title,
            Description = description,
            Columns = columns
        });
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
        _template.Manifest ??= DashboardTemplateManifest.FromModel(_template);
        _template.Context ??= new DashboardTemplateContext();
        _template.Context.Manifest = _template.Manifest;
        _template.Context.Options = _template.Options;
        _template.Context.CurrentTemplate = _template;
        if (_template.Composition is not null)
        {
            _template.Composition.DerivedTemplate = _template;
        }
        return _template;
    }

    private void CopyBaseTemplate(DashboardTemplateModel source)
    {
        _template.Title = source.Title;
        _template.Subtitle = source.Subtitle;
        _template.Description = source.Description;
        _template.LayoutName = source.LayoutName;
        _template.Layout.Title = source.Layout.Title;
        _template.Layout.Subtitle = source.Layout.Subtitle;
        _template.Layout.ShowHeader = source.Layout.ShowHeader;
        _template.Layout.ShowFooter = source.Layout.ShowFooter;
        _template.Layout.Fluid = source.Layout.Fluid;
        _template.Layout.Spacing = source.Layout.Spacing;

        _template.Sections.Clear();
        foreach (var section in source.Sections)
        {
            var copy = CloneSection(section);
            _template.Sections.Add(copy);
            Track(_template.Composition?.InheritedRegions, copy.Name ?? copy.Title);
        }

        foreach (var widget in source.Widgets)
        {
            if (!_template.Widgets.Contains(widget, StringComparer.OrdinalIgnoreCase))
            {
                _template.Widgets.Add(widget);
            }
        }

        CopyResponsiveLayout(source.Context);
    }

    private void CopyResponsiveLayout(DashboardTemplateContext? source)
    {
        if (source is null)
        {
            return;
        }

        _template.Context ??= new DashboardTemplateContext();
        _template.Context.CurrentBreakpoint = source.CurrentBreakpoint;
        _template.Context.Layout.Clear();
        foreach (var item in source.Layout)
        {
            _template.Context.Layout[item.Key] = CloneOptions(item.Value);
        }

        _template.Context.Zones.Clear();
        foreach (var zone in source.Zones)
        {
            var clone = new LayoutRegion
            {
                Name = zone.Name,
                Zone = zone.Zone,
                Options = CloneOptions(zone.Options)
            };
            foreach (var item in zone.Breakpoints)
            {
                clone.Breakpoints[item.Key] = CloneOptions(item.Value);
            }
            _template.Context.Zones.Add(clone);
        }

        var widgets = _template.Sections
            .SelectMany(section => section.Widgets)
            .ToDictionary(widget => widget.Id, StringComparer.OrdinalIgnoreCase);
        _template.Context.Placements.Clear();
        foreach (var placement in source.Placements)
        {
            if (!widgets.TryGetValue(placement.WidgetId, out var widget))
            {
                if (placement.Widget is not WidgetModelBase widgetModel)
                {
                    continue;
                }
                widget = CloneWidget(widgetModel);
            }
            var clone = new WidgetPlacement
            {
                Widget = widget,
                Zone = placement.Zone,
                Region = placement.Region,
                Order = placement.Order,
                Priority = placement.Priority,
                ColumnSpan = placement.ColumnSpan,
                RowSpan = placement.RowSpan,
                MinWidth = placement.MinWidth,
                MaxWidth = placement.MaxWidth
            };
            AddBreakpoints(clone.VisibleOn, placement.VisibleOn);
            AddBreakpoints(clone.HiddenOn, placement.HiddenOn);
            _template.Context.Placements.Add(clone);
        }
    }

    private void AddSlotWidgets(string name, TemplateRegion region, object? content)
    {
        if (content is not WidgetModelBase &&
            content is not IEnumerable<WidgetModelBase>)
        {
            return;
        }

        _template.Context ??= new DashboardTemplateContext();
        if (!_template.Context.NamedSlots.TryGetValue(name, out var slot))
        {
            slot = new TemplateSlot { Name = name, Region = region };
            _template.Context.NamedSlots[name] = slot;
        }

        if (content is WidgetModelBase widget)
        {
            slot.Widgets.Add(widget);
        }
        else if (content is IEnumerable<WidgetModelBase> widgets)
        {
            foreach (var item in widgets)
            {
                slot.Widgets.Add(item);
            }
        }
    }

    private int FindRegionIndex(string name)
    {
        for (var index = 0; index < _template.Sections.Count; index++)
        {
            if (string.Equals(
                _template.Sections[index].Name,
                name,
                StringComparison.OrdinalIgnoreCase))
            {
                return index;
            }
        }

        return -1;
    }

    private DashboardSectionModel FindRegion(string name)
    {
        var index = FindRegionIndex(name);
        return index >= 0
            ? _template.Sections[index]
            : throw new KeyNotFoundException($"Template region '{name}' was not found.");
    }

    private static int FindWidgetIndex(DashboardSectionModel section, string widgetId)
    {
        for (var index = 0; index < section.Widgets.Count; index++)
        {
            if (section.Widgets[index] is WidgetModelBase widget &&
                string.Equals(widget.Id, widgetId, StringComparison.OrdinalIgnoreCase))
            {
                return index;
            }
        }

        return -1;
    }

    private static DashboardSectionModel SetRegion(
        DashboardSectionModel section,
        TemplateRegion region)
    {
        ArgumentNullException.ThrowIfNull(section);
        section.Region = region;
        return section;
    }

    private static TemplateRegion ToTemplateRegion(DashboardSlot slot) =>
        slot switch
        {
            DashboardSlot.Hero => TemplateRegion.Hero,
            DashboardSlot.Header => TemplateRegion.Header,
            DashboardSlot.Toolbar => TemplateRegion.Toolbar,
            DashboardSlot.Sidebar => TemplateRegion.Sidebar,
            DashboardSlot.Footer => TemplateRegion.Footer,
            DashboardSlot.Widgets => TemplateRegion.Widgets,
            _ => TemplateRegion.Content
        };

    private static TemplateRegion ToTemplateRegion(LayoutZone zone) =>
        zone switch
        {
            LayoutZone.Hero => TemplateRegion.Hero,
            LayoutZone.Header => TemplateRegion.Header,
            LayoutZone.Toolbar => TemplateRegion.Toolbar,
            LayoutZone.Sidebar => TemplateRegion.Sidebar,
            LayoutZone.Footer => TemplateRegion.Footer,
            _ => TemplateRegion.Content
        };

    private static DashboardColumns ToDashboardColumns(int? columns) =>
        columns switch
        {
            1 => DashboardColumns.One,
            2 => DashboardColumns.Two,
            3 => DashboardColumns.Three,
            4 => DashboardColumns.Four,
            _ => DashboardColumns.Auto
        };

    private static ResponsiveLayoutOptions ResolveOptions(
        LayoutRegion region,
        ResponsiveBreakpoint breakpoint,
        IDictionary<ResponsiveBreakpoint, ResponsiveLayoutOptions> layout)
    {
        var resolved = CloneOptions(region.Options);
        if (layout.TryGetValue(breakpoint, out var layoutOptions))
        {
            OverlayOptions(resolved, layoutOptions);
        }
        if (region.Breakpoints.TryGetValue(breakpoint, out var breakpointOptions))
        {
            OverlayOptions(resolved, breakpointOptions);
        }
        return resolved;
    }

    private static ResponsiveLayoutOptions CloneOptions(ResponsiveLayoutOptions source) => new()
    {
        Columns = source.Columns,
        Rows = source.Rows,
        Gap = source.Gap,
        Span = source.Span,
        Priority = source.Priority,
        Hidden = source.Hidden,
        Collapsed = source.Collapsed,
        Order = source.Order,
        MinWidth = source.MinWidth,
        MaxWidth = source.MaxWidth,
        Stack = source.Stack
    };

    private static void OverlayOptions(
        ResponsiveLayoutOptions target,
        ResponsiveLayoutOptions source)
    {
        target.Columns = source.Columns ?? target.Columns;
        target.Rows = source.Rows ?? target.Rows;
        target.Gap = source.Gap ?? target.Gap;
        target.Span = source.Span ?? target.Span;
        target.Priority = source.Priority ?? target.Priority;
        target.Hidden = source.Hidden ?? target.Hidden;
        target.Collapsed = source.Collapsed ?? target.Collapsed;
        target.Order = source.Order ?? target.Order;
        target.MinWidth = source.MinWidth ?? target.MinWidth;
        target.MaxWidth = source.MaxWidth ?? target.MaxWidth;
        target.Stack = source.Stack ?? target.Stack;
    }

    private WidgetPlacement FindPlacement(string widgetId) =>
        _template.Context?.Placements.FirstOrDefault(placement =>
            string.Equals(placement.WidgetId, widgetId, StringComparison.OrdinalIgnoreCase))
        ?? throw new KeyNotFoundException($"Widget placement '{widgetId}' was not found.");

    private void RemovePlacement(string widgetId)
    {
        if (_template.Context?.Placements.FirstOrDefault(placement =>
            string.Equals(placement.WidgetId, widgetId, StringComparison.OrdinalIgnoreCase)) is { } existing)
        {
            _template.Context.Placements.Remove(existing);
        }
    }

    private static void AddBreakpoints(
        ICollection<ResponsiveBreakpoint> destination,
        IEnumerable<ResponsiveBreakpoint>? source)
    {
        if (source is null)
        {
            return;
        }
        foreach (var breakpoint in source)
        {
            if (!destination.Contains(breakpoint))
            {
                destination.Add(breakpoint);
            }
        }
    }

    private static DashboardSectionModel CloneSection(DashboardSectionModel source)
    {
        var clone = new DashboardSectionModel
        {
            Name = source.Name,
            Region = source.Region,
            Title = source.Title,
            Description = source.Description,
            Columns = source.Columns
        };
        foreach (var widget in source.Widgets.OfType<WidgetModelBase>())
        {
            clone.Widgets.Add(CloneWidget(widget));
        }
        return clone;
    }

    private static WidgetModelBase CloneWidget(WidgetModelBase source)
    {
        WidgetModelBase clone = source switch
        {
            DashboardHeroWidgetModel hero => CloneHero(hero),
            KpiWidgetModel kpi => CloneKpi(kpi),
            QuickActionsWidgetModel actions => CloneQuickActions(actions),
            ActivityFeedWidgetModel activity => CloneActivityFeed(activity),
            _ => throw new NotSupportedException(
                $"Widget type '{source.GetType().Name}' cannot be inherited by the template composer.")
        };
        clone.Id = source.Id;
        clone.Title = source.Title;
        clone.Subtitle = source.Subtitle;
        clone.CssClass = source.CssClass;
        clone.Visible = source.Visible;
        clone.Size = source.Size;
        clone.Theme = source.Theme;
        clone.State = source.State;
        return clone;
    }

    private static DashboardHeroWidgetModel CloneHero(DashboardHeroWidgetModel source) => new()
    {
        Description = source.Description,
        PrimaryActionText = source.PrimaryActionText,
        PrimaryActionUrl = source.PrimaryActionUrl,
        SecondaryActionText = source.SecondaryActionText,
        SecondaryActionUrl = source.SecondaryActionUrl,
        BackgroundVariant = source.BackgroundVariant
    };

    private static KpiWidgetModel CloneKpi(KpiWidgetModel source) => new()
    {
        Value = source.Value,
        Description = source.Description,
        Trend = source.Trend,
        TrendType = source.TrendType,
        Icon = source.Icon,
        Footer = source.Footer
    };

    private static QuickActionsWidgetModel CloneQuickActions(QuickActionsWidgetModel source)
    {
        var clone = new QuickActionsWidgetModel();
        foreach (var action in source.Actions)
        {
            clone.Actions.Add(new WidgetAction
            {
                Text = action.Text,
                Icon = action.Icon,
                Url = action.Url,
                Variant = action.Variant
            });
        }
        return clone;
    }

    private static ActivityFeedWidgetModel CloneActivityFeed(ActivityFeedWidgetModel source)
    {
        var clone = new ActivityFeedWidgetModel();
        foreach (var item in source.Items)
        {
            clone.Items.Add(new ActivityItem
            {
                Icon = item.Icon,
                Title = item.Title,
                Description = item.Description,
                Timestamp = item.Timestamp,
                Badge = item.Badge,
                Color = item.Color
            });
        }
        return clone;
    }

    private static void Track(ICollection<string>? values, string? value)
    {
        if (values is not null &&
            !string.IsNullOrWhiteSpace(value) &&
            !values.Contains(value, StringComparer.OrdinalIgnoreCase))
        {
            values.Add(value);
        }
    }

    private static void RemoveTracked(ICollection<string>? values, string value)
    {
        if (values is null)
        {
            return;
        }

        var existing = values.FirstOrDefault(item =>
            string.Equals(item, value, StringComparison.OrdinalIgnoreCase));
        if (existing is not null)
        {
            values.Remove(existing);
        }
    }

    private DashboardTemplateBuilder SetTheme(DashboardTheme theme)
    {
        _template.Options!.Theme = theme;
        return this;
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
