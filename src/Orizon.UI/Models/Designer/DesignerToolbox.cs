using Orizon.UI.Factories.Templates;
using Orizon.UI.Models.Templates;

namespace Orizon.UI.Models.Designer;

public sealed record DesignerToolboxItem(string Key, string Name, object Value);
public sealed record DesignerToolboxGroup(string Name, IReadOnlyList<DesignerToolboxItem> Items);

/// <summary>Registry-backed toolbox catalog. It never owns a manual component list.</summary>
public sealed class DesignerToolbox
{
    public IReadOnlyList<DesignerToolboxGroup> Groups { get; private set; } = [];

    internal void Load(IReadOnlyCollection<DashboardTemplateModel> templates, IReadOnlyCollection<ITemplateFactory> factories)
    {
        var manifests = factories.Select(factory => factory.GetManifest()).ToArray();
        Groups =
        [
            Group("Widgets", templates.SelectMany(template => template.Context?.Placements ?? [])
                .GroupBy(placement => placement.Widget.GetType())
                .Select(group => Item(group.Key.FullName!, group.Key.Name.Replace("WidgetModel", ""), group.First().Widget))),
            Group("Layouts", manifests.Where(manifest => !string.IsNullOrWhiteSpace(manifest.LayoutName))
                .GroupBy(manifest => manifest.LayoutName!, StringComparer.OrdinalIgnoreCase)
                .Select(group => Item(group.Key, group.Key, group.First()))),
            Group("Templates", templates.Select(template => Item(template.Name, template.DisplayName ?? template.Name, template))),
            Group("Zones", templates.SelectMany(template => template.Context?.Zones ?? [])
                .GroupBy(zone => zone.Zone)
                .Select(group => Item(group.Key.ToString(), group.Key.ToString(), group.First()))),
            Group("Slots", templates.SelectMany(template => template.Context?.NamedSlots.Values ?? [])
                .GroupBy(slot => slot.Name, StringComparer.OrdinalIgnoreCase)
                .Select(group => Item(group.Key, group.Key, group.First())))
        ];
    }

    private static DesignerToolboxItem Item(string key, string name, object value) => new(key, name, value);
    private static DesignerToolboxGroup Group(string name, IEnumerable<DesignerToolboxItem> items) =>
        new(name, items.OrderBy(item => item.Name, StringComparer.OrdinalIgnoreCase).ToArray());
}
