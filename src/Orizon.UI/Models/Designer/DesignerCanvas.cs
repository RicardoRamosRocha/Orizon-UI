using Orizon.UI.Models.Dashboard;
using Orizon.UI.Models.Layout;
using Orizon.UI.Models.Templates;

namespace Orizon.UI.Models.Designer;

/// <summary>Mutable in-memory projection of the template loaded in Studio.</summary>
public sealed class DesignerCanvas
{
    public DashboardTemplateModel? Template { get; private set; }
    public DashboardLayoutModel? Layout => Template?.Layout;
    public IReadOnlyList<DesignerZone> Zones { get; private set; } = [];
    public IReadOnlyList<DesignerWidget> Widgets { get; private set; } = [];
    public IReadOnlyList<TemplateSlot> Slots { get; private set; } = [];

    public void Load(DashboardTemplateModel template)
    {
        Template = template ?? throw new ArgumentNullException(nameof(template));
        var context = template.Context;
        Widgets = context?.Placements
            .Select(placement => new DesignerWidget { Model = placement.Widget, Placement = placement })
            .ToArray() ?? [];
        Slots = context?.NamedSlots.Values.OrderBy(slot => slot.Name).ToArray() ?? [];
        Zones = context?.Zones.Select(region => new DesignerZone
        {
            Region = region,
            Widgets = Widgets.Where(widget =>
                widget.Placement?.Zone == region.Zone &&
                (string.IsNullOrWhiteSpace(widget.Placement.Region) ||
                 string.Equals(widget.Placement.Region, region.Name, StringComparison.OrdinalIgnoreCase)))
                .ToArray(),
            Slots = Slots.Where(slot => string.Equals(
                slot.Region.ToString(), region.Name, StringComparison.OrdinalIgnoreCase)).ToArray()
        }).ToArray() ?? [];
    }

    public WidgetPlacement FindPlacement(string widgetId) =>
        Template?.Context?.Placements.FirstOrDefault(item =>
            string.Equals(item.WidgetId, widgetId, StringComparison.OrdinalIgnoreCase))
        ?? throw new KeyNotFoundException($"Widget '{widgetId}' is not present on the canvas.");

    public void Insert(WidgetPlacement placement, int order)
    {
        var context = RequireContext();
        context.Placements.Add(placement);
        SetPlacement(placement, placement.Zone, placement.Region, order);
    }

    public void Remove(WidgetPlacement placement)
    {
        RequireContext().Placements.Remove(placement);
        Recalculate();
    }

    public void Move(WidgetPlacement placement, LayoutZone zone, string region, int order)
    {
        SetPlacement(placement, zone, region, order);
    }

    public void Recalculate()
    {
        var template = Template ?? throw new InvalidOperationException("No template is loaded.");
        var context = RequireContext();
        var breakpoint = context.CurrentBreakpoint ?? ResponsiveBreakpoint.Desktop;

        foreach (var placement in context.Placements)
        {
            if (placement.Order is null) placement.Order = int.MaxValue;
        }

        foreach (var group in context.Placements.GroupBy(item => (item.Zone, item.Region)))
        {
            var order = 0;
            foreach (var placement in group.OrderBy(item => item.Order).ThenByDescending(item => item.Priority))
                placement.Order = order++;
        }

        template.Sections.Clear();
        foreach (var zone in context.Zones
            .Select((region, index) => new { Region = region, Index = index, Options = ResolveOptions(region, breakpoint) })
            .Where(item => item.Options.Hidden != true)
            .OrderBy(item => item.Options.Order ?? item.Index))
        {
            foreach (var group in context.Placements
                .Where(item => item.Zone == zone.Region.Zone && item.IsVisible(breakpoint))
                .OrderBy(item => item.Order)
                .ThenByDescending(item => item.Priority)
                .GroupBy(item => string.IsNullOrWhiteSpace(item.Region) ? zone.Region.Name : item.Region!))
            {
                var section = new DashboardSectionModel
                {
                    Name = group.Key,
                    Title = group.Key,
                    Columns = ToColumns(zone.Options.Stack == true ? 1 : zone.Options.Columns)
                };
                foreach (var placement in group) section.Widgets.Add(placement.Widget);
                template.Sections.Add(section);
            }
        }
        Load(template);
    }

    private void SetPlacement(WidgetPlacement placement, LayoutZone zone, string? region, int order)
    {
        var siblings = RequireContext().Placements
            .Where(item => item != placement && item.Zone == zone &&
                string.Equals(item.Region, region, StringComparison.OrdinalIgnoreCase))
            .OrderBy(item => item.Order).ToArray();
        order = Math.Clamp(order, 0, siblings.Length);
        placement.Zone = zone;
        placement.Region = region;
        placement.Order = order;
        for (var index = 0; index < siblings.Length; index++)
            siblings[index].Order = index < order ? index : index + 1;
        Recalculate();
    }

    private DashboardTemplateContext RequireContext() =>
        Template?.Context ?? throw new InvalidOperationException("The loaded template has no responsive context.");

    private static ResponsiveLayoutOptions ResolveOptions(LayoutRegion region, ResponsiveBreakpoint breakpoint) =>
        region.Breakpoints.TryGetValue(breakpoint, out var options) ? options : region.Options;

    private static DashboardColumns ToColumns(int? columns) => columns switch
    {
        <= 1 => DashboardColumns.One,
        2 => DashboardColumns.Two,
        3 => DashboardColumns.Three,
        _ => DashboardColumns.Four
    };
}
