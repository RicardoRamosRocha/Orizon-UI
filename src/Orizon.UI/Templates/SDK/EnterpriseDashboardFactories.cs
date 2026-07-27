using Orizon.UI.Enums.Templates;
using Orizon.UI.Factories.Templates;
using Orizon.UI.Models.Templates;
using Orizon.UI.Models.Layout;
using Orizon.UI.Templates.Dashboard;

namespace Orizon.UI.Templates.SDK;

public sealed class ExecutiveDashboardFactory : ITemplateFactory
{
    public DashboardTemplateModel Create() => ExecutiveDashboardTemplate.Create();
    public bool CanCreate() => true;
    public DashboardTemplateManifest GetManifest() => EnterpriseDashboardManifests.Executive();
}

public sealed class OperationsDashboardFactory : ITemplateFactory
{
    public DashboardTemplateModel Create() => OperationsDashboardTemplate.Create();
    public bool CanCreate() => true;
    public DashboardTemplateManifest GetManifest() => EnterpriseDashboardManifests.Operations();
}

public sealed class AnalyticsDashboardFactory : ITemplateFactory
{
    public DashboardTemplateModel Create() => AnalyticsDashboardTemplate.Create();
    public bool CanCreate() => true;
    public DashboardTemplateManifest GetManifest() => EnterpriseDashboardManifests.Analytics();
}

public sealed class WorkspaceDashboardFactory : ITemplateFactory
{
    public DashboardTemplateModel Create() => WorkspaceDashboardTemplate.Create();
    public bool CanCreate() => true;
    public DashboardTemplateManifest GetManifest() => EnterpriseDashboardManifests.Workspace();
}

public sealed class BlankDashboardFactory : ITemplateFactory
{
    public DashboardTemplateModel Create() => BlankDashboardTemplate.Create();
    public bool CanCreate() => true;
    public DashboardTemplateManifest GetManifest() => EnterpriseDashboardManifests.Blank();
}

internal static class EnterpriseDashboardManifests
{
    public static DashboardTemplateManifest Executive()
    {
        var manifest = Create(
            ExecutiveDashboardTemplate.Name,
            "Executive Dashboard",
            "Executive",
            "Template genérico para indicadores e acompanhamento executivo.",
            "#312e81",
            true,
            ["Dashboard Hero", "KPI", "Activity Feed", "Quick Actions"]);
        manifest.SupportsComposition = true;
        manifest.Composable = true;
        foreach (var region in new[]
        {
            "Hero", "KPIs", "Quick Actions", "Sales Overview",
            "Inventory Overview", "Financial Summary", "Recent Activity", "Tasks"
        })
        {
            manifest.Regions.Add(region);
        }
        manifest.DerivedTemplates.Add(ERPDashboardTemplate.Name);
        return manifest;
    }

    public static DashboardTemplateManifest Operations() => Create(
        OperationsDashboardTemplate.Name,
        "Operations Dashboard",
        "Operations",
        "Template genérico para acompanhamento de rotinas operacionais.",
        "#0f766e",
        false,
        ["KPI", "Timeline", "Widget Container", "Activity Feed"]);

    public static DashboardTemplateManifest Analytics() => Create(
        AnalyticsDashboardTemplate.Name,
        "Analytics Dashboard",
        "Analytics",
        "Template genérico para leitura de métricas sem gráficos adicionais.",
        "#7c3aed",
        false,
        ["KPI", "Widget Container", "Card", "Quick Actions"]);

    public static DashboardTemplateManifest Workspace() => Create(
        WorkspaceDashboardTemplate.Name,
        "Workspace Dashboard",
        "Workspace",
        "Template genérico para organizar uma área de trabalho.",
        "#0369a1",
        false,
        ["Dashboard Hero", "Quick Actions", "Activity Feed", "Widget Container"]);

    public static DashboardTemplateManifest Blank() => Create(
        BlankDashboardTemplate.Name,
        "Blank Dashboard",
        "Foundation",
        "Template mínimo com layout e seção vazia para composição.",
        "#475569",
        false,
        []);

    private static DashboardTemplateManifest Create(
        string name,
        string displayName,
        string category,
        string description,
        string previewColor,
        bool featured,
        IEnumerable<string> widgets)
    {
        var manifest = new DashboardTemplateManifest
        {
            Name = name,
            DisplayName = displayName,
            Category = category,
            Description = description,
            Version = "1.0.0",
            Author = "Orizon UI",
            LayoutName = "DashboardLayout",
            PreviewColor = previewColor,
            Featured = featured,
            SupportsResponsiveLayout = true,
            DefaultBreakpoint = ResponsiveBreakpoint.Desktop,
            PreferredDensity = "Comfortable"
        };
        foreach (var breakpoint in Enum.GetValues<ResponsiveBreakpoint>())
        {
            manifest.SupportedBreakpoints.Add(breakpoint);
        }
        foreach (var zone in new[]
        {
            LayoutZone.Hero, LayoutZone.Main, LayoutZone.Left,
            LayoutZone.Center, LayoutZone.Right, LayoutZone.Bottom
        })
        {
            manifest.DefaultZones.Add(zone);
        }
        manifest.SupportedThemes.Add(DashboardTheme.Default);
        manifest.SupportedThemes.Add(DashboardTheme.Light);
        manifest.SupportedThemes.Add(DashboardTheme.Dark);
        foreach (var widget in widgets)
        {
            manifest.Widgets.Add(widget);
        }
        return manifest;
    }
}
