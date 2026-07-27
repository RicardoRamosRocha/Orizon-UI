using System.Collections.ObjectModel;
using Orizon.UI.Enums.Templates;
using Orizon.UI.Models.Layout;

namespace Orizon.UI.Models.Templates;

/// <summary>
/// Describes a dashboard template without creating or rendering it.
/// </summary>
public sealed class DashboardTemplateManifest
{
    public string Name { get; set; } = string.Empty;
    public string? DisplayName { get; set; }
    public string? Category { get; set; }
    public string? Description { get; set; }
    public string? Version { get; set; }
    public string? Author { get; set; }
    public string? PreviewImage { get; set; }
    public string? PreviewColor { get; set; }
    public Collection<string> Tags { get; } = [];
    public Collection<DashboardTheme> SupportedThemes { get; } = [];
    public string? LayoutName { get; set; }
    public Collection<string> Widgets { get; } = [];
    public string? MinimumVersion { get; set; }
    public bool? Featured { get; set; }
    public bool? Experimental { get; set; }
    public bool? Deprecated { get; set; }
    public string? BaseTemplate { get; set; }
    public bool? SupportsComposition { get; set; }
    public bool? Composable { get; set; }
    public Collection<string> Regions { get; } = [];
    public Collection<string> InheritedWidgets { get; } = [];
    public Collection<string> DerivedTemplates { get; } = [];
    public bool? SupportsResponsiveLayout { get; set; }
    public ResponsiveBreakpoint? DefaultBreakpoint { get; set; }
    public Collection<ResponsiveBreakpoint> SupportedBreakpoints { get; } = [];
    public string? PreferredDensity { get; set; }
    public Collection<LayoutZone> DefaultZones { get; } = [];
    public IDictionary<string, object?> Metadata { get; } =
        new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Creates a manifest from the catalog data exposed by the legacy model.
    /// </summary>
    public static DashboardTemplateManifest FromModel(DashboardTemplateModel model)
    {
        ArgumentNullException.ThrowIfNull(model);

        var manifest = new DashboardTemplateManifest
        {
            Name = model.Name,
            DisplayName = model.DisplayName,
            Category = model.Category,
            Description = model.Description,
            Version = model.Version,
            Author = model.Author,
            PreviewImage = model.PreviewImage,
            LayoutName = model.LayoutName
        };

        AddRange(manifest.Tags, model.Tags);
        AddRange(manifest.Widgets, model.Widgets);
        if (model.Composition is not null)
        {
            manifest.BaseTemplate = model.Composition.BaseTemplate?.Name;
            AddRange(manifest.Regions, model.Sections
                .Select(section => section.Name)
                .Where(name => !string.IsNullOrWhiteSpace(name))
                .Select(name => name!));
        }
        foreach (var item in model.Metadata)
        {
            manifest.Metadata[item.Key] = item.Value;
        }

        return manifest;
    }

    internal void ApplyTo(DashboardTemplateModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
        model.Name = Name;
        model.DisplayName = DisplayName;
        model.Category = Category;
        model.Description = Description;
        model.Version = Version;
        model.Author = Author;
        model.PreviewImage = PreviewImage;
        model.LayoutName = LayoutName;
        Replace(model.Tags, Tags);
        Replace(model.Widgets, Widgets);
        foreach (var item in Metadata)
        {
            model.Metadata[item.Key] = item.Value;
        }
    }

    private static void Replace(ICollection<string> destination, IEnumerable<string> values)
    {
        destination.Clear();
        AddRange(destination, values);
    }

    private static void AddRange<T>(ICollection<T> destination, IEnumerable<T> values)
    {
        foreach (var value in values)
        {
            destination.Add(value);
        }
    }
}
